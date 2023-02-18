using Northgard.Core.Enums;
using Northgard.Interactor.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.Panel;
using Northgard.Presentation.Common.UserInteraction;
using Northgard.Presentation.Common.UserInteraction.GameObjectRelocation;
using Northgard.Presentation.Common.UserInteraction.Select;
using Northgard.Presentation.Common.View;
using Northgard.Presentation.Common.VisualEffects;
using Northgard.Presentation.Common.VisualEffects.GameObjectEffects;
using Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.SelectableBehaviours;
using Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.SelectWorldDirection;
using Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.TerritoryOperations;
using UIToolkit.InteractionHelpers;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.UserInteraction.WorldEditorUserInteraction
{
    public class WEditorUserInteractionManager : MonoBehaviour, IUserInteractionManager
    {
        [Inject] private IWorldEditorController _worldEditorController;
        [Inject] private ITerritoryOperationPanel _territoryOperationPanel;
        [Inject] private ISelectWorldDirectionPanel _directionSelector;
        [Inject] private ISelectorView<TerritoryPrefabViewModel> _territorySelector;
        [Inject] private ISelectorView<NaturalDistrictPrefabViewModel> _naturalDistrictSelector;
        [Inject] private IFocusView _focusPanelHandler;
        [Inject] private ISelectEffect<GameObject> _selectEffect;
        [Inject] private IGameObjectMoveHandler _moveHandler;
        [Inject] private ICommonInput _commonInput;
        public ISelectable CurrentSelectedBehaviour { get; private set; }
        public event ISelectable.SelectableDelegate OnSelect;
        public event ISelectable.SelectableDelegate OnDeselect;
        private SelectableBehaviour<TerritoryViewModel> _currentSelectedTerritory;
        private WorldDirection _currentSelectedDirection;
        private NaturalDistrictPrefabViewModel _currentLocatingNaturalDistrict;

        private void Start()
        {
            _commonInput.PrimaryInputPhysical.OnClick += OnClickAnywhere;
            _worldEditorController.OnTerritoryAdded += MakeTerritorySelectable;
            _worldEditorController.OnNaturalDistrictAdded += MakeNaturalDistrictSelectable;
            _worldEditorController.OnWorldChanged += ResetWorldInteractions;
        }

        private void ResetWorldInteractions()
        {
            if (CurrentSelectedBehaviour != null)
            {
                CurrentSelectedBehaviour.Deselect(_worldEditorController);
            }
            var world = _worldEditorController.CurrentWorld;
            foreach (var territory in world.Territories)
            {
                foreach (var naturalDistrict in territory.NaturalDistricts)
                {
                    MakeNaturalDistrictSelectable(territory, naturalDistrict);
                }
                MakeTerritorySelectable(territory);
            }
        }

        private void MakeTerritorySelectable(TerritoryViewModel territoryViewModel)
        {
            var selectableTerritory = _worldEditorController.AddComponentToTerritory<SelectableTerritory>(territoryViewModel.Id);
            selectableTerritory.Data = territoryViewModel;
            selectableTerritory.OnSelect += OnTerritorySelected;
            selectableTerritory.OnDeselect += OnTerritoryDeselected;
        }
        
        private void MakeNaturalDistrictSelectable(TerritoryViewModel territory, NaturalDistrictViewModel naturalDistrict)
        {
            var selectableNaturalDistrict =
                _worldEditorController.AddComponentToNaturalDistrict<SelectableNaturalDistrict>(naturalDistrict.Id);
            selectableNaturalDistrict.Data = naturalDistrict;
            selectableNaturalDistrict.Territory = territory;
        }
        
        private void OnTerritorySelected(SelectableBehaviour<TerritoryViewModel> selectable)
        {
            _currentSelectedTerritory = selectable;
            var availableDirections = _worldEditorController.GetTerritoryAvailableDirections(selectable.Data.Id);
            _directionSelector.Show(selectable.transform, availableDirections);
            _directionSelector.OnSelect = OnTerritoryDirectionSelected;
            _territoryOperationPanel.Show(selectable.transform, new ITerritoryOperationPanel.TerritoryOperationConfig());
            _territoryOperationPanel.OnNatureClick = ShowAddNaturalDistrictView;
        }

        private void OnTerritoryDeselected(SelectableBehaviour<TerritoryViewModel> selectable)
        {
            _directionSelector.Hide();
            _territoryOperationPanel.Hide();
        }
        
        private void OnTerritoryDirectionSelected(WorldDirection worldDirection)
        {
            DeselectAsset(CurrentSelectedBehaviour, this);
            _currentSelectedDirection = worldDirection;
            _territorySelector.UpdateCaption("Select the territory to add");
            _territorySelector.ShowCloseButton();
            _focusPanelHandler.Focus(_territorySelector);
            _territorySelector.OnConfirm = AddNewTerritory;
        }

        private void AddNewTerritory(TerritoryPrefabViewModel data)
        {
            _currentSelectedTerritory.Deselect(_territorySelector);
            _worldEditorController.NewTerritory(new CreateTerritoryViewModel()
            {
                Direction = _currentSelectedDirection,
                Prefab = data,
                SourceTerritoryId = _currentSelectedTerritory.Data.Id
            });
        }
        
        private void ShowAddNaturalDistrictView()
        {
            DeselectAsset(CurrentSelectedBehaviour, this);
            _naturalDistrictSelector.UpdateCaption("Select the natural district to add");
            _naturalDistrictSelector.ShowCloseButton();
            _focusPanelHandler.Focus(_naturalDistrictSelector);
            _naturalDistrictSelector.OnConfirm = NewNaturalDistrict;
        }

        private void NewNaturalDistrict(NaturalDistrictPrefabViewModel data)
        {
            _currentLocatingNaturalDistrict = data;
            _currentSelectedTerritory.Deselect(_naturalDistrictSelector);
            var fakeObject = _worldEditorController.GenerateFakeNaturalDistrict(data.PrefabId);
            // _selectEffect.StopSelectEffect();
            _selectEffect.PlaySelectEffect(fakeObject);
            var locator = _moveHandler.GenerateLocator(fakeObject, _currentSelectedTerritory.Data.Bounds, _selectEffect.CurrentSelectEffect);
            locator.OnLocated = AddNaturalDistrict;
            locator.OnFinished = delegate
            {
                _currentLocatingNaturalDistrict = null;
            };
        }

        private void AddNaturalDistrict(GameObject locatingObject)
        {
            _worldEditorController.NewNaturalDistrict(new CreateNaturalDistrictViewModel()
            {
                Prefab = _currentLocatingNaturalDistrict,
                TerritoryId = _currentSelectedTerritory.Data.Id,
                Position = locatingObject.transform.position,
                Rotation = locatingObject.transform.rotation
            });
        }


        private void OnClickAnywhere(PointerInputBehaviour asset)
        {
            if (CurrentSelectedBehaviour == null) return;
            var currentAssetInput = CurrentSelectedBehaviour as PointerInputBehaviour;
            if (currentAssetInput == null) return;
            if (currentAssetInput.IsHover) return;
            DeselectAsset(CurrentSelectedBehaviour, this);
        }

        public void SelectAsset<T, TS>(TS selectableBehaviour, object sender) where TS : MonoBehaviour, ISelectableBehaviour<T>
        {
            if (CurrentSelectedBehaviour != null)
            {
                DeselectAsset(CurrentSelectedBehaviour, this);
            }
            CurrentSelectedBehaviour = selectableBehaviour;
            OnSelect?.Invoke(selectableBehaviour);
            if (sender != selectableBehaviour)
            {
                selectableBehaviour.Select(this);
            }
            _selectEffect.PlaySelectEffect(selectableBehaviour.gameObject);
        }

        public void DeselectAsset(ISelectable selectableBehaviour, object sender)
        {
            if (CurrentSelectedBehaviour == selectableBehaviour)
            {
                CurrentSelectedBehaviour = null;
            }
            CurrentSelectedBehaviour = null;
            OnDeselect?.Invoke(selectableBehaviour);
            if (sender != selectableBehaviour)
            {
                selectableBehaviour.Deselect(this);
            }
            _selectEffect.StopSelectEffect();
        }
    }
}