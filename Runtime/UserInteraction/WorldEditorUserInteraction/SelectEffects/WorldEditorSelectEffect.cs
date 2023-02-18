using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.VisualEffects;
using Northgard.Presentation.Common.VisualEffects.GameObjectEffects;
using UnityEngine;

namespace Northgard.Presentation.UserInteraction.WorldEditorUserInteraction.SelectEffects
{
    public class WorldEditorSelectEffect : MonoBehaviour, ISelectEffect<GameObject>
    {
        [SerializeField] private Shader highlightShader;
        public IGameObjectEffect CurrentSelectEffect { get; private set; }

        public void PlaySelectEffect(GameObject objectToSelect)
        {
            CurrentSelectEffect = new HighlightShaderEffect(objectToSelect, highlightShader);
            CurrentSelectEffect.Play(Color.blue);
        }

        public void StopSelectEffect()
        {
            CurrentSelectEffect.Stop();
            CurrentSelectEffect = null;
        }
    }
}