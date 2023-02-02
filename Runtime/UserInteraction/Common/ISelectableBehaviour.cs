using Northgard.Presentation.Common.Select;
using UnityEngine;

namespace Northgard.Presentation.UserInteraction.Common
{
    public interface ISelectableBehaviour<T> : ISelectable
    {
        T Data { get; }
    }
}