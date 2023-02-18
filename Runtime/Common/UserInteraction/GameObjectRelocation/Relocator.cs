using System.Linq;
using Northgard.Presentation.Common.UserInteraction.GameLayer;
using Northgard.Presentation.Common.VisualEffects;
using UnityEngine;
using Zenject;
using ILogger = Northgard.Core.Infrastructure.Logger.ILogger;

namespace Northgard.Presentation.Common.UserInteraction.GameObjectRelocation
{
    public abstract class Relocator : MonoBehaviour, IRelocator
    {
        [Inject] private ILogger _logger;
        [Inject] protected ICommonInput commonInput;
        [Inject] private IGameLayers _gameLayers;
        private static readonly Vector3 OutOfPosition = Vector3.one * -999999;
        private Transform _transform;
        private Collider _collider;
        private bool _isLocated;
        private bool _isCanceled;
        private Bounds _limitBounds;
        private IGameObjectEffect _effect;
        public IRelocator.LocateDelegate OnLocated { set; private get; }
        public IRelocator.FinishDelegate OnFinished { set; private get; }

        public virtual void Initialize(Bounds limitBounds,
            IGameObjectEffect effect)
        {
            _limitBounds = limitBounds;
            _transform = transform;
            _collider = GetComponent<Collider>();
            _effect = effect;
        }
        
        protected virtual void Update()
        {
            _transform.position = CalculatePosition();
            if (CanBeLocated())
            {
                _effect.ChangeColor(Color.green);
            }
            else
            {
                _effect.ChangeColor(Color.red);
            }
        }
        
        protected void Locate()
        {
            if (_isLocated)
            {
                _logger.LogError("Cannot locate while the object has been located already", this);
                return;
            }
            if (_isCanceled)
            {
                _logger.LogError("Cannot locate while the object locating has been canceled", this);
                return;
            }
            if (CanBeLocated() == false)
            {
                _logger.LogError("Cannot be located here", this);
                return;
            }
            _isLocated = true;
            OnLocated?.Invoke(gameObject);
            Finish();
        }
        
        protected virtual void CancelLocating()
        {
            if (_isLocated)
            {
                _logger.LogError("Cannot cancel locating while the object has been located already", this);
                return;
            }
            if (_isCanceled)
            {
                _logger.LogError("Cannot cancel locating while the object locating has been canceled already", this);
                return;
            }
            _isCanceled = true;
            Finish();
        }
 

        protected virtual void Finish()
        {
            Destroy(this);
            OnFinished?.Invoke(gameObject);
        }
        
        protected virtual Vector3 CalculatePosition()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                _logger.LogError("Couldn't find the main camera", this);
                return OutOfPosition;
            }
            var ray = mainCamera.ScreenPointToRay(commonInput.PrimaryInputPhysical.CurrentPointerPosition);
            var hits = Physics.RaycastAll(ray, Mathf.Infinity, _gameLayers.FloorLayerMask);
            hits = hits.Where(h => h.collider.transform != _transform).ToArray();
            if (hits.Length == 0)
            {
                return OutOfPosition;
            }

            var hit = hits.OrderBy(h => h.distance).First();
            var pos = hit.point;
            pos.y += _collider.bounds.extents.y;
            return pos;
        }
        
        protected virtual bool CanBeLocated()
        {
            var bounds = _collider.bounds;
            var extents = bounds.extents;
            var center = bounds.center;
            if (center.x < _limitBounds.min.x + extents.x)
            {
                return false;
            }
            if (center.x > _limitBounds.max.x - extents.x)
            {
                return false;
            }
            if (center.z < _limitBounds.min.z + extents.z)
            {
                return false;
            }

            if (center.z > _limitBounds.max.z - extents.z)
            {
                return false;
            }

            var hitColliders = Physics.OverlapBox(center, extents, Quaternion.identity,
                _gameLayers.All, QueryTriggerInteraction.Ignore);
            hitColliders = hitColliders.Where(h => h.transform != _transform).ToArray();
            if (hitColliders.Length > 0)
            {
                return false;
            }
            return true;
        }
    }
}