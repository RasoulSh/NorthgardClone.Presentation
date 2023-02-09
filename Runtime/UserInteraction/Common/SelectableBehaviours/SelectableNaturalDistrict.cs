using Northgard.Interactor.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.UserInteraction.Common.SelectableBehaviours
{
    public class SelectableNaturalDistrict : SelectableBehaviour<NaturalDistrictViewModel>
    {
        [Inject] private IWorldEditorController _worldEditorController;
        public TerritoryViewModel Territory { get; set; }
        protected override void OnDraggingAction()
        {
            if (IsSelected == false || Territory  == null)
            {
                return;
            }

            var ray = Camera.main.ScreenPointToRay(CurrentPointerPosition);
            var isHit = Physics.Raycast(ray, out RaycastHit hit);
            if (isHit == false)
            {
                return;
            }
            var targetPosition = hit.point;
            _worldEditorController.RepositionNaturalDistrict(Data.Id, Territory.Id, targetPosition);
        }
    }
}