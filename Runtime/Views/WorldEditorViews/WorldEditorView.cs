using System;
using Northgard.Interactor.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.Panel;
using Northgard.Presentation.Common.View;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.Views.WorldEditorViews
{
    internal class WorldEditorView : View
    {
        [Inject] private IWorldEditorController _worldEditorController;
        [Inject] private ISelectorView<WorldPrefabViewModel> _worldSelector;
        [Inject] private ISelectorView<TerritoryPrefabViewModel> _territorySelector;
        [Inject] private IFocusView _focusPanelHandler;

        private void Start()
        {
            IsInteractable = false;
            _worldSelector.UpdateCaption("Select the world");
            _focusPanelHandler.Focus(_worldSelector);
            _worldSelector.OnConfirm = SelectWorld;
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
            IsInteractable = true;
        }
    }
}