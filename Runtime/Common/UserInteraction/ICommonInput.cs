using UIToolkit.InteractionHelpers;

namespace Northgard.Presentation.Common.UserInteraction
{
    public interface ICommonInput
    {
        public PointerInputBehaviour PrimaryInputAll { get; }
        public PointerInputBehaviour SecondaryInputAll { get; }
        public PointerInputBehaviour PrimaryInputPhysical { get; }
    }
}