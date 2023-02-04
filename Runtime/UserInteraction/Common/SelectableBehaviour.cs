using UIToolkit.InteractionHelpers;
using Zenject;

namespace Northgard.Presentation.UserInteraction.Common
{
    public abstract class SelectableBehaviour<T> : ColliderInputBehaviour, ISelectableBehaviour<T>
    {
        [Inject] private IUserInteractionManager _userInteractionManager;
        public T Data { get; set; }
        public bool IsSelected { get; private set; }
        public event SelectableDelegate OnSelect;
        public event SelectableDelegate OnDeselect;
        public delegate void SelectableDelegate(SelectableBehaviour<T> selectable);

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
            OnSelect?.Invoke(this);
        }

        public void Deselect(object sender)
        {
            if (IsSelected == false) return;
            IsSelected = false;
            if (sender != _userInteractionManager)
            {
                _userInteractionManager.DeselectAsset(this, this);
            }
            OnDeselect?.Invoke(this);
        }
    }
}