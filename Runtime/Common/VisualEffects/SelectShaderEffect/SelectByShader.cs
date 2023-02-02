using UnityEngine;

namespace Northgard.Presentation.Common.VisualEffects.SelectShaderEffect
{ 
        public class SelectByShader
        {
            private readonly Renderer[] _meshRenderers;
            private readonly Material[] _originalMaterials;
            private readonly Shader _hoverShader;
            public bool IsSelected { get; private set; }
            
            public SelectByShader(GameObject hoverObject, Shader hoverShader)
            {
                _hoverShader = hoverShader;
                _meshRenderers = hoverObject.GetComponentsInChildren<Renderer>();
                _originalMaterials = new Material[_meshRenderers.Length];
            }
            

            public void PlaySelectEffect()
            {
                if (IsSelected) return;
                IsSelected = true;
                if (_meshRenderers == null) return;
                var i = 0;
                foreach (var meshRenderer in _meshRenderers)
                {
                    if (meshRenderer != null)
                    {
                        Material originalMaterial = meshRenderer.material;
                        Material cloneMaterial = Object.Instantiate(originalMaterial);
                        cloneMaterial.shader = _hoverShader;
                        _originalMaterials[i] = originalMaterial;
                        meshRenderer.material = cloneMaterial;
                    }
                    i++;
                }
            }
            public void PlayDeselectEffect()
            {
                if (IsSelected == false) return;
                IsSelected = false;
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
        }
}