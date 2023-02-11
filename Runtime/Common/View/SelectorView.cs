using System.Collections.Generic;
using Northgard.Core.UnityExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace Northgard.Presentation.Common.View
{
    internal abstract class SelectorView<T,TS> : ClosableView, ISelectorView<T> where TS : SelectableSubView<T>
    {
        [SerializeField] private Text captionLabel;
        [SerializeField] private GridLayoutGroup selectGrid;
        [SerializeField] private Button confirmButton;
        [SerializeField] private bool hideOnConfirm;
        protected abstract TS ItemPrefab { get; }
        protected abstract IEnumerable<T> DataItems { get; }
        private TS _currentSelectedItem;
        public ISelectorView<T>.ConfirmDelegate OnConfirm { private get; set; }

        protected virtual void Start()
        {
            confirmButton.onClick.AddListener(Confirm);
            confirmButton.interactable = false;
        }

        private void Confirm()
        {
            if (hideOnConfirm)
            {
                Hide();
            }
            OnConfirm?.Invoke(_currentSelectedItem.Data);
        }

        public override void UpdateView()
        {
            _currentSelectedItem = null;
            confirmButton.interactable = false;
            var gridTransform = selectGrid.transform;
            gridTransform.DestroyAllChildren();
            foreach (var dataItem in DataItems)
            {
                var newSubView = ItemPrefab.Instantiate(dataItem) as TS;
                newSubView!.transform.SetParent(gridTransform);
                newSubView!.OnSelect += ChangeSelectedItem;
            }
        }

        private void ChangeSelectedItem(SelectableSubView<T> item)
        {
            if (_currentSelectedItem != null)
            {
                _currentSelectedItem.Deselect(this);
            }
            _currentSelectedItem = item as TS;
            confirmButton.interactable = true;
        }

        public void UpdateCaption(string caption)
        {
            captionLabel.text = caption;
        }
    }
}