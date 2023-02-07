using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.Panel;
using Northgard.Presentation.Common.View;
using Northgard.Presentation.UserInteraction.Common;
using Northgard.Presentation.UserInteraction.WorldEditorUserInteraction;
using Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.SelectWorldDirection;
using Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.TerritoryOperations;
using Northgard.Presentation.Views.WorldEditorViews;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation
{
    [RequireComponent(typeof(WEditorUserInteractionManager))]
    public class WorldEditorInstaller : MonoInstaller
    {
        [SerializeField] private TerritoryOperationPanel territoryOperationPanel;
        [SerializeField] private SelectWorldDirectionPanel worldDirectionPanel;
        [SerializeField] private WEditorSelectWorldView selectWorldView;
        [SerializeField] private WEditorSelectTerritoryView selectTerritoryView;
        [SerializeField] private WEditorSelectNaturalDistrictView selectNaturalDistrictView;
        [SerializeField] private FocusView FocusPanelHandler;
        public override void InstallBindings()
        {
            var userInteractionManager = GetComponent<WEditorUserInteractionManager>();

            Container.Bind<IFocusView>().To<FocusView>().FromInstance(FocusPanelHandler).AsSingle();
            Container.Bind<ITerritoryOperationPanel>().To<TerritoryOperationPanel>()
                .FromInstance(territoryOperationPanel).AsSingle();
            Container.Bind<ISelectWorldDirectionPanel>().To<SelectWorldDirectionPanel>()
                .FromInstance(worldDirectionPanel).AsSingle();
            Container.Bind<IUserInteractionManager>().To<WEditorUserInteractionManager>()
                .FromInstance(userInteractionManager).AsSingle();
            Container.Bind<ISelectorView<WorldPrefabViewModel>>().To<WEditorSelectWorldView>().FromInstance(selectWorldView).AsSingle();
            Container.Bind<ISelectorView<TerritoryPrefabViewModel>>().To<WEditorSelectTerritoryView>().FromInstance(selectTerritoryView).AsSingle();
            Container.Bind<ISelectorView<NaturalDistrictPrefabViewModel>>().To<WEditorSelectNaturalDistrictView>()
                .FromInstance(selectNaturalDistrictView).AsSingle();
        }
    }
}