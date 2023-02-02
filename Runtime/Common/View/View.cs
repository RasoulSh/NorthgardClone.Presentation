using UIToolkit.GUIPanelSystem;
using UnityEngine;

namespace Northgard.Presentation.Common.View
{
    [RequireComponent(typeof(GUIPanel))]
    internal abstract class View : MonoBehaviour, IView
    {
        private GUIPanel _panel;
        private GUIPanel Panel => _panel ??= GetComponent<GUIPanel>();
        public bool IsShown => Panel.IsShown;

        public bool IsInteractable
        {
            get => _panel.IsInteractable;
            set => _panel.IsInteractable = value;
        }

        public void Show() => Toggle(true);
        
        public void Hide() => Toggle(false);

        protected virtual void Awake()
        {
            if (Panel.IsShown)
            {
                UpdateView();
            }
        }

        public void Toggle(bool isShown)
        {
            if (isShown)
            {
                UpdateView();
            }
            Panel.Toggle(isShown);
        }
        public abstract void UpdateView();
    }
}