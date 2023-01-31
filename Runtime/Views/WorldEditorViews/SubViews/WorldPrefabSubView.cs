using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.View;
using UnityEngine;
using UnityEngine.UI;

namespace Northgard.Presentation.Views.WorldEditorViews.SubViews
{
    public class WorldPrefabSubView : SubView<WorldPrefabViewModel>
    {
        [SerializeField] private Text titleLabel;

        public override void UpdateView()
        {
            titleLabel.text = Data.Title;
        }
    }
}