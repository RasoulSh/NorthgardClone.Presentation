namespace Northgard.Presentation.Common.View
{
    internal interface ISelectorView<T> : IView
    {
        public event ConfirmDelegate OnConfirm;
        public delegate void ConfirmDelegate(T data);
    }
}