using Northgard.Interactor.Abstraction;
using Northgard.Interactor.Enums.WorldEnums;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.Panel;
using Northgard.Presentation.Common.Select;
using Northgard.Presentation.Common.View;
using Northgard.Presentation.Common.VisualEffects.SelectShaderEffect;
using Northgard.Presentation.UserInteraction.Common;
using Northgard.Presentation.UserInteraction.Common.SelectableBehaviours;
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
        [SerializeField] private MouseInputBehaviour mouseInput;
        [SerializeField] private Shader selectShader;
        public ISelectable CurrentSelectedBehaviour { get; private set; }
        public event ISelectable.SelectableDelegate OnSelect;
        public event ISelectable.SelectableDelegate OnDeselect;
        private SelectByShader _currentSelectEffect;
        private SelectableBehaviour<TerritoryViewModel> _currentSelectedTerritory;
        private WorldDirection _currentSelectedDirection;

        private void Start()
        {
            mouseInput.OnClick += OnClickAnywhere;
            _worldEditorController.OnTerritoryAdded += MakeTerritorySelectable;
            _worldEditorController.OnNaturalDistrictAdded += MakeNaturalDistrictSelectable;
            _worldEditorController.OnWorldLoaded += MakeWorldInteractable;
        }

        private void MakeWorldInteractable()
        {
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
            _naturalDistrictSelector.OnConfirm = AddNaturalDistrict;
        }

        private void AddNaturalDistrict(NaturalDistrictPrefabViewModel data)
        {
            _currentSelectedTerritory.Deselect(_naturalDistrictSelector);
            _worldEditorController.NewNaturalDistrict(new CreateNaturalDistrictViewModel()
            {
                Prefab = data,
                TerritoryId = _currentSelectedTerritory.Data.Id
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
            _currentSelectEffect = new SelectByShader(selectableBehaviour.gameObject, selectShader);
            _currentSelectEffect.PlaySelectEffect();
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
            _currentSelectEffect.PlayDeselectEffect();
            _currentSelectEffect = null;
        }
    }
}