using System.Linq;
using Northgard.Presentation.Common.UserInteraction.GameLayer;
using Northgard.Presentation.Common.VisualEffects;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.Common.UserInteraction.GameObjectRelocation
{
    public interface IRelocator
    {
        LocateDelegate OnLocated { set; }
        FinishDelegate OnFinished { set; }
        public delegate void LocateDelegate(GameObject locatingObject);
        public delegate void FinishDelegate(GameObject locatingObject);

        public void Initialize(Bounds limitBounds, IGameObjectEffect effect);
    }
}