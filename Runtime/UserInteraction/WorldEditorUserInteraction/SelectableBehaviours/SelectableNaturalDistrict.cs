using Northgard.Interactor.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.UserInteraction.GameObjectRelocation;
using Northgard.Presentation.Common.UserInteraction.Select;
using Northgard.Presentation.Common.VisualEffects;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.SelectableBehaviours
{
    public class SelectableNaturalDistrict : SelectableBehaviour<NaturalDistrictViewModel>
    {
        [Inject] private IWorldEditorController _worldEditorController;
        [Inject] private IGameObjectMoveHandler _moveHandler;
        [Inject] private ISelectEffect<GameObject> _selectEffect;
        public TerritoryViewModel Territory { get; set; }
        private IGameObjectDragLocator _locator;

        protected override void OnStartDraggingAction()
        {
            if (IsSelected == false)
            {
                return;
            }
            _locator = _moveHandler.GenerateDragLocator(gameObject, Territory.Bounds, _selectEffect.CurrentSelectEffect);
            _locator.OnLocated = Locate;
            _locator.OnFinished = OnFinishLocating;
        }

        private void OnFinishLocating(GameObject locatingObject)
        {
            _locator = null;
            if (_selectEffect.CurrentSelectEffect == null)
            {
                return;
            }
            _selectEffect.CurrentSelectEffect.ChangeColor(Color.blue);
        }

        private void Locate(GameObject locatingObject)
        {
            _worldEditorController.RepositionNaturalDistrict(Data.Id, Territory.Id, transform.position);
        }
    }
}