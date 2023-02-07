using System;
using System.Collections;
using Northgard.Presentation.Common.View;
using TweenerSystem;
using TweenerSystem.Enums;
using UIToolkit.GUIPanelSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Northgard.Presentation.Common.Panel
{
    internal class FocusView : MonoBehaviour, IFocusView
    {
        [SerializeField] private Tweener focusTweener;
        [SerializeField] private Graphic graphic;
        private FocusPanel _currentFocusView;
        private Coroutine _currentPlayingRoutine;

        private void Awake()
        {
            graphic.enabled = false;
        }

        public void Focus(IView view)
        {
            if (view.IsShown == true)
            {
                Debug.LogWarning("Cannot focus on a panel which is showing");
                return;
            }
            if (_currentFocusView != null)
            {
                Debug.LogWarning("You cannot focus on a panel while another panel is being focused");
                return;
            }
            StopCurrentPlayingRoutine();
            view.OnToggle = OnFocusPanelToggle;
            var panelTransform = view.Panel.transform;
            _currentFocusView = new FocusPanel()
            {
                Panel = view.Panel,
                OriginalParent = panelTransform.parent,
                SiblingIndex = view.Panel.transform.GetSiblingIndex()
            };
            focusTweener.Play(TweenerDirection.Backward, true);
            graphic.enabled = true;
            _currentPlayingRoutine = StartCoroutine(FocusRoutine(view));
        }

        private IEnumerator FocusRoutine(IView view)
        {
            var panelTransform = view.Panel.transform;
            focusTweener.PlayForward();
            yield return new WaitForSeconds(focusTweener.TotalDuration);
            panelTransform.SetParent(transform, true);
            yield return null;
            view.Toggle(true);
        }

        private void OnFocusPanelToggle(IView view)
        {
            if (view.IsShown)
            {
                return;
            }
            StopCurrentPlayingRoutine();
            _currentPlayingRoutine = StartCoroutine(UnfocusRoutine());
        }
        
        private IEnumerator UnfocusRoutine()
        {
            _currentFocusView.BackToOriginalParent();
            _currentFocusView = null;
            focusTweener.PlayBackward();
            yield return new WaitForSeconds(focusTweener.TotalDuration);
            graphic.enabled = false;
        }

        private void StopCurrentPlayingRoutine()
        {
            if (_currentPlayingRoutine != null)
            {
                StopCoroutine(_currentPlayingRoutine);
                _currentPlayingRoutine = null;
            }
        }

        private class FocusPanel
        {
            public GUIPanel Panel { private get; set; }
            public Transform OriginalParent { private get; set; }
            public int SiblingIndex { private get; set; }

            public void BackToOriginalParent()
            {
                Panel.transform.SetParent(OriginalParent, true);
                Panel.transform.SetSiblingIndex(SiblingIndex);
            }
        }
    }
}