using UnityEngine;

namespace Northgard.Presentation.Common.VisualEffects
{
    public interface IGameObjectEffect
    {
        bool IsPlaying { get; }
        void Play();
        void Play(Color initialColor);
        void Stop();
        void ChangeColor(Color color);
    }
}