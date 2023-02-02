using System;
using System.Collections.Generic;
using Northgard.Interactor.Abstraction;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Northgard.Presentation.Common.Select;
using Northgard.Presentation.Common.VisualEffects.SelectShaderEffect;
using Northgard.Presentation.UserInteraction.Common;
using Northgard.Presentation.UserInteraction.Common.SelectableBehaviours;
using UIToolkit.InteractionHelpers;
using UnityEngine;
using Zenject;

namespace Northgard.Presentation.UserInteraction
{
    public class WEditorUserInteractionManager : MonoBehaviour, IUserInteractionManager
    {
        [Inject] private IWorldEditorController _worldEditorController;
        [SerializeField] private MouseInputBehaviour mouseInput;
        [SerializeField] private Shader selectShader;
        public ISelectable CurrentSelectedBehaviour { get; private set; }
        public event ISelectable.SelectableDelegate OnSelect;
        public event ISelectable.SelectableDelegate OnDeselect;
        private SelectByShader _currentSelectEffect;

        private void Start()
        {
            mouseInput.OnClick += OnClickedAnywhere;
            _worldEditorController.OnTerritoryAdded += MakeTerritorySelectable;
        }

        private void MakeTerritorySelectable(TerritoryViewModel territoryViewModel)
        {
            var selectableTerritory = _worldEditorController.AddComponentToTerritory<SelectableTerritory>(territoryViewModel.Id);
            selectableTerritory.Data = territoryViewModel;
        }

        private void OnClickedAnywhere(PointerInputBehaviour asset)
        {
            if (CurrentSelectedBehaviour == null) return;
            var currentAssetInput = CurrentSelectedBehaviour as PointerInputBehaviour;
            if (currentAssetInput == null) return;
            if (currentAssetInput.IsHover) return;
            DeselectAsset(CurrentSelectedBehaviour, this);
        }

        public void SelectAsset<T, TS>(TS selectableBehaviour, object sender) where TS : MonoBehaviour, ISelectableBehaviour<T>
        {
            if (CurrentSelectedBehaviour != null)
            {
                DeselectAsset(CurrentSelectedBehaviour, this);
            }
            CurrentSelectedBehaviour = selectableBehaviour;
            OnSelect?.Invoke(selectableBehaviour);
            if (sender != selectableBehaviour)
            {
                selectableBehaviour.Select(this);
            }
            _currentSelectEffect = new SelectByShader(selectableBehaviour.gameObject, selectShader);
            _currentSelectEffect.PlaySelectEffect();
        }

        public void DeselectAsset(ISelectable selectableBehaviour, object sender)
        {
            if (CurrentSelectedBehaviour == selectableBehaviour)
            {
                CurrentSelectedBehaviour = null;
            }
            CurrentSelectedBehaviour = null;
            OnDeselect?.Invoke(selectableBehaviour);
            if (sender != selectableBehaviour)
            {
                selectableBehaviour.Deselect(this);
            }
            _currentSelectEffect.PlayDeselectEffect();
            _currentSelectEffect = null;
        }
    }
}