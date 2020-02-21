using System;
using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Tooling
{
    public class InputGroundTracker : MonoBehaviour
    {
        public InputGroundTrackerConfig config;

        public InputGroundTrackerState state;

        private RaycastHit[] _raycastHits;

        private RaycastHit2D[] _raycastHits2D;

        public void Start()
        {
            foreach (var layerName in config.layerNames)
            {
                var mask = LayerMask.NameToLayer(layerName);
                if (mask < 0)
                {
                    Debug.Log($"No matching layer found for name '{layerName}'; ignoring");
                }
                else
                {
                    state.layerMask |= mask;
                }
            }

            foreach (var volume in config.queryVolumes)
            {
                volume.active = true;
                volume.layerMask = state.layerMask;
            }
        }

        public void Update()
        {
            switch (config.trackerMode)
            {
                case InputGroundTrackerMode.Track3D:
                    Update3D();
                    break;
                case InputGroundTrackerMode.Track2D:
                    Update2D();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update2D()
        {
            if (_raycastHits2D == null)
            {
                _raycastHits2D = new RaycastHit2D[1];
            }

            var grounded = false;
            foreach (var source in config.queryPoints)
            {
                var matches = Physics2D.RaycastNonAlloc(source.transform.position, Down2, _raycastHits2D, config.queryDistance, state.layerMask);
                if (matches > 0)
                {
                    grounded = true;
                    break;
                }
            }

            foreach (var volume in config.queryVolumes)
            {
                if (volume.connected > 0)
                {
                    grounded = true;
                    break;
                }
            }
            
            if (grounded)
            {
                state.timeSinceLastGrounded = 0;
                state.grounded = true;
            }
            else
            {
                state.grounded = false;
                state.timeSinceLastGrounded += Time.deltaTime;
            }
        }

        private void Update3D()
        {
            if (_raycastHits == null)
            {
                _raycastHits = new RaycastHit[1];
            }

            var grounded = false;
            foreach (var source in config.queryPoints)
            {
                var ray = new Ray(source.transform.position, Down3);
                var matches = Physics.RaycastNonAlloc(ray, _raycastHits, config.queryDistance, state.layerMask);
                if (matches > 0)
                {
                    grounded = true;
                    break;
                }
            }

            foreach (var volume in config.queryVolumes)
            {
                if (volume.connected > 0)
                {
                    grounded = true;
                    break;
                }
            }

            if (grounded)
            {
                state.timeSinceLastGrounded = 0;
                state.grounded = true;
            }
            else
            {
                state.grounded = false;
                state.timeSinceLastGrounded += Time.deltaTime;
            }
        }

        private Vector2 Down2
        {
            get
            {
                if (config.directionReference == null)
                {
                    return Vector2.down;
                }

                return -config.directionReference.transform.up;
            }
        }

        private Vector3 Down3
        {
            get
            {
                if (config.directionReference == null)
                {
                    return Vector3.down;
                }

                return -config.directionReference.transform.up;
            }
        }

        public enum InputGroundTrackerMode
        {
            Track3D,
            Track2D,
        }

        [System.Serializable]
        public class InputGroundTrackerState
        {
            [Tooltip("Read-only; use layerName;")] public int layerMask = -5;

            public float timeSinceLastGrounded;

            public bool grounded;
        }

        [System.Serializable]
        public class InputGroundTrackerConfig
        {
            [Tooltip("Use this object for the axis to find 'down'; or Vector3 if null")]
            public GameObject directionReference;

            [Tooltip("Raycast query points to find the ground")]
            public List<GameObject> queryPoints;

            [Tooltip("Collision volumes to find the ground with")]
            public List<InputGroundTrackerVolume> queryVolumes;
            
            public InputGroundTrackerMode trackerMode;

            public float queryDistance = 0.5f;

            [Tooltip("Automatically look up named layers instead of using a mask by putting the names here")]
            public List<string> layerNames;
        }
    }
}