using Northgard.Interactor.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.Panel;
using Northgard.Presentation.Common.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Northgard.Presentation.Views.WorldEditorViews
{
    internal class WorldEditorView : View
    {
        [Inject] private IWorldEditorController _worldEditorController;
        [Inject] private ISelectorView<WorldPrefabViewModel> _worldSelector;
        [Inject] private ISelectorView<TerritoryPrefabViewModel> _territorySelector;
        [Inject] private IFocusView _focusPanelHandler;
        [SerializeField] private Button newWorldButton;
        [SerializeField] private Button loadWorldButton;
        [SerializeField] private Button saveWorldButton;
        private const string TempSaveName = "TempWorld";

        private void Start()
        {
            newWorldButton.onClick.AddListener(NewWorld);
            loadWorldButton.onClick.AddListener(LoadWorld);
            saveWorldButton.onClick.AddListener(SaveWorld);
            saveWorldButton.interactable = false;
        }

        private void NewWorld()
        {
            _worldSelector.UpdateCaption("Select the world");
            if (_worldEditorController.CurrentWorld != null)
            {
                _worldSelector.ShowCloseButton();
            }
            _focusPanelHandler.Focus(_worldSelector);
            _worldSelector.OnConfirm = SelectWorld;
        }

        private void LoadWorld()
        {
            _worldEditorController.LoadWorld(TempSaveName);
            saveWorldButton.interactable = true;
        }

        private void SaveWorld()
        {
            _worldEditorController.SaveWorld(TempSaveName);
        }

        public override void UpdateView()
        {
            
        }
        
        private void SelectWorld(WorldPrefabViewModel data)
        {
            _worldEditorController.SelectWorld(new SelectWorldViewModel() { Prefab = data });
            _territorySelector.UpdateCaption("Select the first territory");
            _focusPanelHandler.Focus(_territorySelector);
            _territorySelector.OnConfirm = SelectFirstTerritory;
        }

        private void SelectFirstTerritory(TerritoryPrefabViewModel data)
        {
            _worldEditorController.SelectFirstTerritory(new SelectFirstTerritoryViewModel() { Prefab = data });
            saveWorldButton.interactable = true;
        }
    }
}