using Northgard.Presentation.Common.UserInteraction.Select;
using UnityEngine;
using UnityEngine.UI;

namespace Northgard.Presentation.Common.View
{
    internal abstract class SelectableSubView<T> : SubView<T>, ISelectable
    {
        [SerializeField] private Button selectButton;
        public bool IsSelected { get; private set; }
        public event SelectDelegate OnSelect;
        public event SelectDelegate OnDeselect;
        public delegate void SelectDelegate(SelectableSubView<T> item);

        protected virtual void Start()
        {
            PlayDeselectEffect(true);
            selectButton.onClick.AddListener(Select);
        }

        private void Select() => Select(this);

        public void Select(object sender)
        {
            IsSelected = true;
            OnSelect?.Invoke(this);
            PlaySelectEffect();
        }

        public void Deselect(object sender)
        {
            IsSelected = false;
            OnDeselect?.Invoke(this);
            PlayDeselectEffect();
        }
        
        protected abstract void PlaySelectEffect(bool ignoreAnimate = false);
        
        protected abstract void PlayDeselectEffect(bool ignoreAnimate = false);
    }
}