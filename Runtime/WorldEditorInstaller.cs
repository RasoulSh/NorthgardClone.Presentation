using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.Panel;
using Northgard.Presentation.Common.UserInteraction;
using Northgard.Presentation.Common.UserInteraction.GameLayer;
using Northgard.Presentation.Common.UserInteraction.GameObjectRelocation;
using Northgard.Presentation.Common.View;
using Northgard.Presentation.Common.VisualEffects;
using Northgard.Presentation.UserInteraction.WorldEditorUserInteraction;
using Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.SelectEffects;
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
        [SerializeField] private GameLayers gameLayers;
        [SerializeField] private TerritoryOperationPanel territoryOperationPanel;
        [SerializeField] private SelectWorldDirectionPanel worldDirectionPanel;
        [SerializeField] private WEditorSelectWorldView selectWorldView;
        [SerializeField] private WEditorSelectTerritoryView selectTerritoryView;
        [SerializeField] private WEditorSelectNaturalDistrictView selectNaturalDistrictView;
        [SerializeField] private WorldEditorSelectEffect selectEffect;
        [SerializeField] private WorldEditorCommonInput commonInput;
        [SerializeField] private FocusView focusPanelHandler;
        public override void InstallBindings()
        {
            var userInteractionManager = GetComponent<WEditorUserInteractionManager>();
            Container.Bind<IGameLayers>().To<GameLayers>().FromInstance(gameLayers).AsSingle();
            Container.Bind<ICommonInput>().To<WorldEditorCommonInput>().FromInstance(commonInput).AsSingle();
            Container.Bind<IGameObjectMoveHandler>().To<GameObjectMoveHandler>().FromNew().AsSingle();
            Container.Bind<ISelectEffect<GameObject>>().To<WorldEditorSelectEffect>().FromInstance(selectEffect)
                .AsSingle();
            Container.Bind<IFocusView>().To<FocusView>().FromInstance(focusPanelHandler).AsSingle();
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