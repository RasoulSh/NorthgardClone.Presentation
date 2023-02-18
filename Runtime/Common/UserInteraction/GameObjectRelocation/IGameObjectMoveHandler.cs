using Northgard.Presentation.Common.VisualEffects;
using UnityEngine;

namespace Northgard.Presentation.Common.UserInteraction.GameObjectRelocation
{
    public interface IGameObjectMoveHandler
    {
        IGameObjectLocator GenerateLocator(GameObject go, Bounds limitBounds,
            IGameObjectEffect effect);
        IGameObjectDragLocator GenerateDragLocator(GameObject go, Bounds limitBounds,
            IGameObjectEffect effect);
    }
}