using System.Collections.Generic;
using JetBrains.Annotations;
using Northgard.Presentation.Common.VisualEffects;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.Common.UserInteraction.GameObjectRelocation
{
    [UsedImplicitly]
    public class GameObjectMoveHandler : IGameObjectMoveHandler
    {
        [Inject] private DiContainer _container;
        
        public IGameObjectLocator GenerateLocator(GameObject go, Bounds limitBounds,
            IGameObjectEffect effect)
        {
            var locator = _container.InstantiateComponent<GameObjectLocator>(go);
            locator.Initialize(limitBounds, effect);
            return locator;
        }
        
        public IGameObjectDragLocator GenerateDragLocator(GameObject go, Bounds limitBounds,
            IGameObjectEffect effect)
        {
            var locator = _container.InstantiateComponent<GameObjectDragLocator>(go);
            locator.Initialize(limitBounds, effect);
            return locator;
        }
    }
}