using UnityEngine;
using UnityEngine.UI;

namespace Northgard.Presentation.Common.View
{
    internal abstract class ClosableView : View, IClosableView
    {
        [SerializeField] private Button closeButton;

        protected override void Awake()
        {
            closeButton.gameObject.SetActive(false);
            closeButton.onClick.AddListener(Hide);
            base.Awake();
        }

        public override void Toggle(bool isShown)
        {
            base.Toggle(isShown);
            if (isShown == false)
            {
                closeButton.gameObject.SetActive(false);
            }
        }

        public void ShowCloseButton()
        {
            closeButton.gameObject.SetActive(true);
        }
    }
}