using System;
using Northgard.Presentation.Common.GUIPanelSystem;
using UnityEngine;

namespace Northgard.Presentation.Common.View
{
    [RequireComponent(typeof(GUIPanel))]
    public abstract class View : MonoBehaviour
    {
        private GUIPanel _panel;
        private GUIPanel Panel => _panel ??= GetComponent<GUIPanel>();
        public bool IsShown => Panel.IsShown;
        
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