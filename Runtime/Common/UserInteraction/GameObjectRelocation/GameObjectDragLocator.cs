using Northgard.Presentation.Common.VisualEffects;
using UIToolkit.InteractionHelpers;
using UnityEngine;

namespace Northgard.Presentation.Common.UserInteraction.GameObjectRelocation
{
    public class GameObjectDragLocator : Relocator, IGameObjectDragLocator
    {
        private Vector3 _lastPosition;
        private void Start()
        {
            commonInput.PrimaryInputAll.OnRelease += OnPrimaryRelease;
            commonInput.SecondaryInputAll.OnPush += OnSecondaryClick;
        }

        public override void Initialize(Bounds limitBounds, IGameObjectEffect effect)
        {
            base.Initialize(limitBounds, effect);
            _lastPosition = transform.position;
        }

        private void OnSecondaryClick(PointerInputBehaviour asset)
        {
            CancelLocating();
        }

        private void OnPrimaryRelease(PointerInputBehaviour asset)
        {
            if (CanBeLocated())
            {
                Locate();
                return;
            }
            CancelLocating();
        }

        protected override void CancelLocating()
        {
            transform.position = _lastPosition;
            base.CancelLocating();
        }

        protected override void Finish()
        {
            commonInput.PrimaryInputAll.OnRelease -= OnPrimaryRelease;
            commonInput.SecondaryInputAll.OnPush -= OnSecondaryClick;
            base.Finish();
        }
    }
}