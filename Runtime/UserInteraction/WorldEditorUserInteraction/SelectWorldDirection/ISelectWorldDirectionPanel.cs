using System.Collections.Generic;
using Northgard.Core.Enums;
using UnityEngine;

namespace Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.SelectWorldDirection
{
    internal interface ISelectWorldDirectionPanel
    {
        SelectDelegate OnSelect { set; }
        public delegate void SelectDelegate(WorldDirection worldDirection);
        void Show(Transform target, IEnumerable<WorldDirection> availableDirections);
        void Hide();
    }
}