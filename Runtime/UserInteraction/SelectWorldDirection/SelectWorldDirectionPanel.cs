﻿using System.Collections.Generic;
using System.Linq;
using Northgard.Interactor.Enums.WorldEnums;
using UIToolkit.GUIPanelSystem;
using UnityEngine;

namespace Northgard.Presentation.UserInteraction.SelectWorldDirection
{
    [RequireComponent(typeof(GUIPanel))]
    internal class SelectWorldDirectionPanel : MonoBehaviour, ISelectWorldDirectionPanel
    {
        [SerializeField] private BillboardWorldDirectionSet[] billboards;
        public ISelectWorldDirectionPanel.SelectDelegate OnSelect { private get; set; }
        private GUIPanel _panel;

        private void Start()
        {
            _panel = GetComponent<GUIPanel>();
            foreach (var billboardSet in billboards)
            {
                billboardSet.Initialize();
                billboardSet.OnClick += SelectDirection;
            }
        }

        private void SelectDirection(BillboardWorldDirectionSet directionSet)
        {
            OnSelect?.Invoke(directionSet.WorldDirection);
            Hide();
        }

        public void Show(Transform target, IEnumerable<WorldDirection> availableDirections)
        {
            var allowedDirections = availableDirections as WorldDirection[] ?? availableDirections.ToArray();
            foreach (var billboardSet in billboards)
            {
                billboardSet.Billboard.Config.Target = target;
                var isAvailable = allowedDirections.Contains(billboardSet.WorldDirection);
                billboardSet.Billboard.CanShow = isAvailable;
            }
            _panel.Toggle(true);
        }

        public void Hide()
        {
            _panel.Toggle(false);
        }
    }
}