using Northgard.Presentation.Common.Select;
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

        public void Select()
        {
            IsSelected = true;
            OnSelect?.Invoke(this);
            PlaySelectEffect();
        }

        public void Deselect()
        {
            IsSelected = false;
            OnDeselect?.Invoke(this);
            PlayDeselectEffect();
        }
        
        protected abstract void PlaySelectEffect(bool ignoreAnimate = false);
        
        protected abstract void PlayDeselectEffect(bool ignoreAnimate = false);
    }
}