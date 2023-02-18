using Northgard.Presentation.Common.UserInteraction;
using UIToolkit.InteractionHelpers;
using UnityEngine;

namespace Northgard.Presentation.UserInteraction.WorldEditorUserInteraction
{
    public class WorldEditorCommonInput : MonoBehaviour, ICommonInput
    {
        [SerializeField] private PointerInputBehaviour primaryInputAll;
        [SerializeField] private PointerInputBehaviour secondaryInputAll;
        [SerializeField] private PointerInputBehaviour primaryInputPhysical;
        public PointerInputBehaviour PrimaryInputAll => primaryInputAll;
        public PointerInputBehaviour SecondaryInputAll => secondaryInputAll;
        public PointerInputBehaviour PrimaryInputPhysical => primaryInputPhysical;
    }
}