namespace Northgard.Presentation.Common.View
{
    internal interface ISelectorView<T> : IView
    {
        public ConfirmDelegate OnConfirm { set; }
        public delegate void ConfirmDelegate(T data);

        void UpdateCaption(string caption);
    }
}