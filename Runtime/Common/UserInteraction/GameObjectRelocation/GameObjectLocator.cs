using UIToolkit.InteractionHelpers;

namespace Northgard.Presentation.Common.UserInteraction.GameObjectRelocation
{
    public class GameObjectLocator : Relocator, IGameObjectLocator
    {
        private void Start()
        {
            commonInput.PrimaryInputPhysical.OnClick += OnPrimaryClick;
            commonInput.SecondaryInputAll.OnPush += OnSecondaryClick;
        }

        private void OnSecondaryClick(PointerInputBehaviour asset)
        {
            CancelLocating();
        }

        private void OnPrimaryClick(PointerInputBehaviour asset)
        {
            if (CanBeLocated() == false)
            {
                return;
            }
            Locate();
        }

        protected override void Finish()
        {
            base.Finish();
            commonInput.PrimaryInputPhysical.OnClick -= OnPrimaryClick;
            commonInput.SecondaryInputAll.OnPush -= OnSecondaryClick;
            Destroy(gameObject);
        }
    }
}