using System.Collections.Generic;
using Northgard.Interactor.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.View;
using Northgard.Presentation.Views.WorldEditorViews.SubViews;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.Views.WorldEditorViews
{
    internal class WEditorSelectTerritoryView : SelectorView<TerritoryPrefabViewModel, TerritoryPrefabSubView>
    {
        [Inject] private IWorldEditorController _worldEditorController;
        [SerializeField] private TerritoryPrefabSubView itemPrefab;
        protected override TerritoryPrefabSubView ItemPrefab => itemPrefab;
        protected override IEnumerable<TerritoryPrefabViewModel> DataItems => _worldEditorController.TerritoryPrefabs;
    }
}