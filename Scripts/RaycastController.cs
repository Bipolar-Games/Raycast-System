using System;
using UnityEngine;

namespace Bipolar.RaycastSystem
{
    public abstract class RayProvider : MonoBehaviour
    {
        public abstract Ray GetRay();
    }

    public class RaycastController : MonoBehaviour
    {
        public event Action<RaycastTarget> OnRayEntered;
        public event Action<RaycastTarget> OnRayExited;

        [Header("Settings")]
        [SerializeField]
        private RayProvider rayProvider;
        public RayProvider RayProvider
        {
            get => rayProvider;
            set
            {
                rayProvider = value;
            }
        }

        [SerializeField]
        private LayerMask raycastedLayers;
        public LayerMask RaycastedLayers 
        { 
            get => raycastedLayers; 
            set => raycastedLayers = value; 
        }
        
        [SerializeField]
        private float raycastDistance;
        public float RaycastDistance
        {
            get => raycastDistance;
            set => raycastDistance = value;
        }
        
        [Header("States")]     
        [SerializeField]
        private RaycastTarget currentTarget;
        public RaycastTarget Target => currentTarget;

        private Ray ray;

        private void Update()
        {
            DoRaycast();
        }

        private void DoRaycast()
        {
            RayProvider provider = rayProvider;
            ray = provider.GetRay();
            if (TryGetRaycastTarget(ray, out var target))
            {
                if (target != currentTarget)
                {
                    CallExitEvents(currentTarget);
                    currentTarget = target;
                    CallEnterEvents(currentTarget);
                }
                else
                {
                    currentTarget.RayStay();
                }
            }
            else
            {
                ExitCurrentTarget();
            }
        }

        private void ExitCurrentTarget()
        {
            if (currentTarget != null)
            {
                var exitedTarget = currentTarget;
                currentTarget = null;
                CallExitEvents(exitedTarget);
            }
        }

        private bool TryGetRaycastTarget(Ray ray, out RaycastTarget target)
        {
            target = null;
            if (Physics.Raycast(ray, out var hit, raycastDistance, raycastedLayers) == false)
                return false;

            if (hit.collider.TryGetComponent<RaycastCollider>(out var raycastCollider) == false)
                return false;

            return TryGetRaycastTarget(raycastCollider, out target);
        }

        static bool TryGetRaycastTarget(RaycastCollider collider, out RaycastTarget target)
        {
            target = collider.RaycastTarget;
            return target != null;
        }

        private void CallEnterEvents(RaycastTarget target)
        {
            if (target != null)
            {
                OnRayEntered?.Invoke(target);
                target.RayEnter();
            }
        }

        private void CallExitEvents(RaycastTarget target)
        {
            if (target != null)
            {
                OnRayExited?.Invoke(target);
                target.RayExit();
            }
        }

        private void OnDisable()
        {
            ExitCurrentTarget();
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(ray.origin, ray.direction * raycastDistance);
        }
    }
}
