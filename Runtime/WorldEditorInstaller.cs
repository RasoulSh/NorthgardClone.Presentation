﻿using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.View;
using Northgard.Presentation.UserInteraction;
using Northgard.Presentation.UserInteraction.Common;
using Northgard.Presentation.Views.WorldEditorViews;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation
{
    [RequireComponent(typeof(WEditorUserInteractionManager))]
    public class WorldEditorInstaller : MonoInstaller
    {
        [SerializeField] private WEditorSelectWorldView selectWorldView;
        [SerializeField] private WEditorSelectTerritoryView selectTerritoryView;
        public override void InstallBindings()
        {
            var userInteractionManager = GetComponent<WEditorUserInteractionManager>();
            
            Container.Bind<IUserInteractionManager>().To<WEditorUserInteractionManager>()
                .FromInstance(userInteractionManager).AsSingle();
            Container.Bind<ISelectorView<WorldPrefabViewModel>>().To<WEditorSelectWorldView>().FromInstance(selectWorldView).AsSingle();
            Container.Bind<ISelectorView<TerritoryPrefabViewModel>>().To<WEditorSelectTerritoryView>().FromInstance(selectTerritoryView).AsSingle();
        }
    }
}