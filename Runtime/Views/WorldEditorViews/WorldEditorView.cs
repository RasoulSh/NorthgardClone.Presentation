using System;
using Northgard.Interactor.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.View;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.Views.WorldEditorViews
{
    internal class WorldEditorView : View
    {
        [Inject] private IWorldEditorController _worldEditorController;
        [Inject] private ISelectorView<WorldPrefabViewModel> _worldSelector;

        private void Start()
        {
            IsInteractable = false;
            _worldSelector.Show();
            _worldSelector.OnConfirm += SelectWorld;
        }

        public override void UpdateView()
        {
            
        }
        
        private void SelectWorld(WorldPrefabViewModel data)
        {
            _worldEditorController.SelectWorld(new SelectWorldViewModel() { Prefab = data });
            IsInteractable = true;
        }
    }
}