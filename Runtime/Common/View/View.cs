using UIToolkit.GUIPanelSystem;
using UnityEngine;

namespace Northgard.Presentation.Common.View
{
    [RequireComponent(typeof(GUIPanel))]
    internal abstract class View : MonoBehaviour, IView
    {
        private GUIPanel _panel;
        public GUIPanel Panel => _panel ??= GetComponent<GUIPanel>();
        public bool IsShown => Panel.IsShown;

        public bool IsInteractable
        {
            get => _panel.IsInteractable;
            set => _panel.IsInteractable = value;
        }
        
        public IView.ViewDelegate OnToggle { private get; set; }

        public void Show() => Toggle(true);
        
        public void Hide() => Toggle(false);

        protected virtual void Awake()
        {
            if (Panel.IsShown)
            {
                UpdateView();
            }
        }

        public virtual void Toggle(bool isShown)
        {
            if (IsShown == isShown)
            {
                return;
            }
            if (isShown)
            {
                UpdateView();
            }
            Panel.Toggle(isShown);
            OnToggle?.Invoke(this);
        }
        public abstract void UpdateView();
    }
}