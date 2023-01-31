using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.View;
using Northgard.Presentation.Views.WorldEditorViews;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation
{
    public class WorldEditorInstaller : MonoInstaller
    {
        [SerializeField] private WEditorSelectWorldView selectWorldView;
        public override void InstallBindings()
        {
            Container.Bind<ISelectorView<WorldPrefabViewModel>>().To<WEditorSelectWorldView>().FromInstance(selectWorldView).AsSingle();
        }
    }
}