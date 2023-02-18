using UIToolkit.InteractionHelpers;
using Zenject;

namespace Northgard.Presentation.Common.UserInteraction.Select
{
    public abstract class SelectableBehaviour<T> : ColliderInputBehaviour, ISelectableBehaviour<T>
    {
        [Inject] protected IUserInteractionManager userInteractionManager;
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
            if (sender != userInteractionManager)
            {
                userInteractionManager.SelectAsset<T, SelectableBehaviour<T>>(this, this);
            }
            OnSelect?.Invoke(this);
        }

        public void Deselect(object sender)
        {
            if (IsSelected == false) return;
            IsSelected = false;
            if (sender != userInteractionManager)
            {
                userInteractionManager.DeselectAsset(this, this);
            }
            OnDeselect?.Invoke(this);
        }
    }
}