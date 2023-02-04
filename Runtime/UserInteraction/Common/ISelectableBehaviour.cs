using Northgard.Presentation.Common.Select;

namespace Northgard.Presentation.UserInteraction.Common
{
    public interface ISelectableBehaviour<T> : ISelectable
    {
        T Data { get; }
    }
}