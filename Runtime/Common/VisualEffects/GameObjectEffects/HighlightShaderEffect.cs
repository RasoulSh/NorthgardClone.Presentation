using UnityEngine;

namespace Northgard.Presentation.Common.VisualEffects.GameObjectEffects
{ 
        public class HighlightShaderEffect : IGameObjectEffect
        {
            private readonly Renderer[] _meshRenderers;
            private readonly Material[] _originalMaterials;
            private readonly Shader _highlightShader;
            private static readonly int HighlightColor = Shader.PropertyToID("_HighlightColor");
            public bool IsPlaying { get; private set; }
            private Color _currentColor = Color.red;
            
            public HighlightShaderEffect(GameObject highlightObject, Shader highlightShader)
            {
                _highlightShader = highlightShader;
                _meshRenderers = highlightObject.GetComponentsInChildren<Renderer>();
                _originalMaterials = new Material[_meshRenderers.Length];
            }
            
            public void Play(Color initialColor)
            {
                ChangeColor(initialColor);
                Play();
            }


            public void Play()
            {
                if (IsPlaying) return;
                IsPlaying = true;
                if (_meshRenderers == null) return;
                var i = 0;
                foreach (var meshRenderer in _meshRenderers)
                {
                    if (meshRenderer != null)
                    {
                        Material originalMaterial = meshRenderer.material;
                        Material cloneMaterial = Object.Instantiate(originalMaterial);
                        cloneMaterial.shader = _highlightShader;
                        _originalMaterials[i] = originalMaterial;
                        meshRenderer.material = cloneMaterial;
                        meshRenderer.material.SetColor(HighlightColor, _currentColor);
                    }
                    i++;
                }
            }
            
            public void Stop()
            {
                if (IsPlaying == false) return;
                IsPlaying = false;
                if (_meshRenderers == null) return;
                var i = 0;
                foreach (var meshRenderer in _meshRenderers)
                {
                    if(meshRenderer != null)
                    {
                        meshRenderer.material = _originalMaterials[i];
                    }
                    i++;
                }
            }

            public void ChangeColor(Color color)
            {
                _currentColor = color;
                foreach (var meshRenderer in _meshRenderers)
                {
                    if (meshRenderer != null)
                    {
                        meshRenderer.material.SetColor(HighlightColor, color);
                    }
                }
            }
        }
}