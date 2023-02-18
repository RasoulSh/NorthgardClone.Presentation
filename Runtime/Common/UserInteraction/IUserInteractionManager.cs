using Northgard.Presentation.Common.UserInteraction.Select;
using Northgard.Presentation.Common.VisualEffects.GameObjectEffects;
using UnityEngine;

namespace Northgard.Presentation.Common.UserInteraction
{
    public interface IUserInteractionManager
    {
        ISelectable CurrentSelectedBehaviour { get; }
        event ISelectable.SelectableDelegate OnSelect;
        event ISelectable.SelectableDelegate OnDeselect;

        void SelectAsset<T, TS>(TS selectableBehaviour, object sender)
            where TS : MonoBehaviour, ISelectableBehaviour<T>;
        void DeselectAsset(ISelectable selectableBehaviour, object sender);
    }
}