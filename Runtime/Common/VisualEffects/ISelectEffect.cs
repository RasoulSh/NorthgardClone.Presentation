using Northgard.Core.EntityBase;
using Northgard.Presentation.Common.UserInteraction.Select;
using UnityEngine;

namespace Northgard.Presentation.Common.VisualEffects
{
    public interface ISelectEffect<T>
    {
        void PlaySelectEffect(T objectToSelect);
        void StopSelectEffect();
        IGameObjectEffect CurrentSelectEffect { get; }
    }
}