using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.View;
using TweenerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Northgard.Presentation.Views.WorldEditorViews.SubViews
{
    internal class TerritoryPrefabSubView : SelectableSubView<TerritoryPrefabViewModel>
    {
        [SerializeField] private Text titleLabel;
        [SerializeField] private Tweener selectTweener;

        public override void UpdateView()
        {
            titleLabel.text = Data.Title;
        }

        protected override void PlaySelectEffect(bool ignoreAnimate = false)
        {
            if (selectTweener != null)
            {
                selectTweener.Play(true, ignoreAnimate);
            }
        }

        protected override void PlayDeselectEffect(bool ignoreAnimate = false)
        {
            if (selectTweener != null)
            {
                selectTweener.Play(false, ignoreAnimate);
            }
        }
    }
}