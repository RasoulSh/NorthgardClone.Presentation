using UnityEngine;

namespace Northgard.Presentation.Common.UserInteraction.GameLayer
{
    public interface IGameLayers
    {
        LayerMask All { get; }
        LayerMask FloorLayerMask { get; }
    }
}