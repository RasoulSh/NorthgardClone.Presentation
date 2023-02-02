using Northgard.Presentation.Common.Select;
using Northgard.Presentation.Common.VisualEffects.SelectShaderEffect;
using Northgard.Presentation.UserInteraction.Common;
using UIToolkit.InteractionHelpers;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.UserInteraction
{
    public abstract class SelectableBehaviour<T> : ColliderInputBehaviour, ISelectableBehaviour<T>
    {
        [Inject] private IUserInteractionManager _userInteractionManager;
        public T Data { get; set; }
        public bool IsSelected { get; private set; }

        protected override void OnClickAction()
        {
            Select(this);
        }

        public void Select(object sender)
        {
            if (IsSelected) return;
            IsSelected = true;
            if (sender != _userInteractionManager)
            {
                _userInteractionManager.SelectAsset<T, SelectableBehaviour<T>>(this, this);
            }
        }

        public void Deselect(object sender)
        {
            if (IsSelected == false) return;
            IsSelected = false;
            if (sender != _userInteractionManager)
            {
                _userInteractionManager.DeselectAsset(this, this);
            }
        }
    }
}