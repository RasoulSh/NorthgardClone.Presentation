namespace Northgard.Presentation.Common.View
{
    internal interface IView
    {
        bool IsShown { get; }
        bool IsInteractable { get; set; }
        void Show();
        void Hide();
        void Toggle(bool isShown);
        void UpdateView();
    }
}