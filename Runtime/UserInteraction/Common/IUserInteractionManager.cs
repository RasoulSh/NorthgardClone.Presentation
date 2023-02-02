using Northgard.Presentation.Common.Select;
using UnityEngine;

namespace Northgard.Presentation.UserInteraction.Common
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