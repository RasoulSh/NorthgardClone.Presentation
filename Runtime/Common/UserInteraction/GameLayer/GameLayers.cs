using UnityEngine;

namespace Northgard.Presentation.Common.UserInteraction.GameLayer
{
    public class GameLayers : MonoBehaviour, IGameLayers
    {
        [SerializeField] private LayerMask all;
        [SerializeField] private LayerMask floorLayerMask;
        public LayerMask All => all;
        public LayerMask FloorLayerMask => floorLayerMask;
    }
}