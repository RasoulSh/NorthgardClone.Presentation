using System;
using Northgard.Interactor.Enums.WorldEnums;
using UIToolkit.Billboard;
using UnityEngine;
using UnityEngine.UI;

namespace Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.SelectWorldDirection
{
    [Serializable]
    internal class BillboardWorldDirectionSet
    {
        [SerializeField] private Billboard billboard;
        [SerializeField] private Button button;
        [SerializeField] private WorldDirection worldDirection;

        public Billboard Billboard => billboard;

        public void Initialize()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke(this);
        }

        public WorldDirection WorldDirection => worldDirection;

        public delegate void DirectionSetDelegate(BillboardWorldDirectionSet directionSet);

        public event DirectionSetDelegate OnClick;
    }
}