using System.Collections.Generic;
using Northgard.Interactor.Enums.WorldEnums;
using UnityEngine;

namespace Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.TerritoryOperations
{
    internal interface ITerritoryOperationPanel
    {
        ClickDelegate OnEditClick { set; }
        ClickDelegate OnDeleteClick { set; }
        ClickDelegate OnNatureClick { set; }
        delegate void ClickDelegate();
        public void Show(Transform target, TerritoryOperationConfig config);
        void Hide();
        
        public class TerritoryOperationConfig
        {
            public bool DeleteInteractable { get; set; }
            public bool EditInteractable { get; set; }
            public bool NatureInteractable { get; set; }
        }
    }
}