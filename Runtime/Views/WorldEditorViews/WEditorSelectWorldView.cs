using Northgard.Core.Common.UnityExtensions;
using Northgard.Interactor.Abstraction;
using Northgard.Presentation.Common.View;
using Northgard.Presentation.Views.WorldEditorViews.SubViews;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Northgard.Presentation.Views.WorldEditorViews
{
    public class WEditorSelectWorldView : View
    {
        [Inject] private IWorldEditorController _worldEditorController;
        [SerializeField] private WorldPrefabSubView subViewPrefab;
        [SerializeField] private GridLayoutGroup grid;

        public override void UpdateView()
        {
            var gridTransform = grid.transform;
            gridTransform.DestroyAllChildren();
            foreach (var worldPrefab in _worldEditorController.WorldPrefabs)
            {
                var newSubView = subViewPrefab.Instantiate(worldPrefab);
                newSubView.transform.SetParent(gridTransform);
            }
        }
    }
}