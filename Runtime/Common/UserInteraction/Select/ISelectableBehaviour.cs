namespace Northgard.Presentation.Common.UserInteraction.Select
{
    public interface ISelectableBehaviour<T> : ISelectable
    {
        T Data { get; }
    }
}