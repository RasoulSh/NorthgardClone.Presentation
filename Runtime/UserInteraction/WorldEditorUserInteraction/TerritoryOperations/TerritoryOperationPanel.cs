using UIToolkit.Billboard;
using UIToolkit.GUIPanelSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.TerritoryOperations
{
    [RequireComponent(typeof(GUIPanel))]
    internal class TerritoryOperationPanel : MonoBehaviour, ITerritoryOperationPanel
    {
        private GUIPanel _panel;
        private GUIPanel Panel => _panel ??= GetComponent<GUIPanel>();
        [SerializeField] private Billboard billboard;
        [SerializeField] private Button editButton;
        [SerializeField] private Button deleteButton;
        [SerializeField] private Button natureButton;
        public ITerritoryOperationPanel.ClickDelegate OnEditClick { private get; set; }
        public ITerritoryOperationPanel.ClickDelegate OnDeleteClick { private get; set; }
        public ITerritoryOperationPanel.ClickDelegate OnNatureClick { private get; set; }
        
        private void Start()
        {
            editButton.onClick.AddListener(delegate { OnEditClick?.Invoke(); });
            deleteButton.onClick.AddListener(delegate { OnDeleteClick?.Invoke(); });
            natureButton.onClick.AddListener(delegate { OnNatureClick?.Invoke(); });
            billboard.ShowBillboard();
        }
        
        public void Show(Transform target, ITerritoryOperationPanel.TerritoryOperationConfig config)
        {
            billboard.Config.Target = target;
            Panel.Toggle(true);
        }

        public void Hide()
        {
            Panel.Toggle(false);
        }
    }
}