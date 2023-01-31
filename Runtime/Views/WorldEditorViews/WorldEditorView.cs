using Northgard.Interactor.Abstraction;
using Northgard.Presentation.Common.View;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.Views.WorldEditorViews
{
    public class WorldEditorView : View
    {
        [Inject] private IWorldEditorController _worldEditorController;
        
        public override void UpdateView()
        {
            throw new System.NotImplementedException();
        }
    }
}