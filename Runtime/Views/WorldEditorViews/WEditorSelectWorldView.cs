using System.Collections.Generic;
using Northgard.Interactor.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.View;
using Northgard.Presentation.Views.WorldEditorViews.SubViews;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.Views.WorldEditorViews
{
    internal class WEditorSelectWorldView : SelectorView<WorldPrefabViewModel, WorldPrefabSubView>
    {
        [Inject] private IWorldEditorController _worldEditorController;
        [SerializeField] private WorldPrefabSubView itemPrefab;
        protected override WorldPrefabSubView ItemPrefab => itemPrefab;
        protected override IEnumerable<WorldPrefabViewModel> DataItems => _worldEditorController.WorldPrefabs;
    }
}