using System.Collections.Generic;
using Northgard.Interactor.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.View;
using Northgard.Presentation.Views.WorldEditorViews.SubViews;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.Views.WorldEditorViews
{
    internal class WEditorSelectNaturalDistrictView : SelectorView<NaturalDistrictPrefabViewModel, NaturalDistrictPrefabSubView>
    {
        [Inject] private IWorldEditorController _worldEditorController;
        [SerializeField] private NaturalDistrictPrefabSubView itemPrefab;
        protected override NaturalDistrictPrefabSubView ItemPrefab => itemPrefab;
        protected override IEnumerable<NaturalDistrictPrefabViewModel> DataItems => _worldEditorController.NaturalDistrictPrefabs;
    }
}