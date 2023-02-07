using UIToolkit.GUIPanelSystem;

namespace Northgard.Presentation.Common.View
{
    internal interface IView
    {
        bool IsShown { get; }
        bool IsInteractable { get; set; }
        GUIPanel Panel { get; }
        void Show();
        void Hide();
        void Toggle(bool isShown);
        void UpdateView();
        ViewDelegate OnToggle { set; }
        public delegate void ViewDelegate(IView view);
    }
}