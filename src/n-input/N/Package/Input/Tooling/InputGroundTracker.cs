using System;
using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Tooling
{
    public class InputGroundTracker : MonoBehaviour
    {
        public InputGroundTrackerConfig config;

        public InputGroundTrackerState state;

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
            var grounded = false;
            foreach (var volume in config.queryVolumes)
            {
                if (volume.connected <= 0) continue;
                grounded = true;
                break;
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
            var grounded = false;
            foreach (var volume in config.queryVolumes)
            {
                if (volume.connected <= 0) continue;
                grounded = true;
                break;
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

        public enum InputGroundTrackerMode
        {
            Track3D,
            Track2D,
        }

        [System.Serializable]
        public class InputGroundTrackerState
        {
            [Tooltip("Read-only; use layerName;")] 
            public int layerMask = -5;

            public float timeSinceLastGrounded;

            public bool grounded;
        }

        [System.Serializable]
        public class InputGroundTrackerConfig
        {
            [Tooltip("Collision volumes to find the ground with")]
            public List<InputGroundTrackerVolume> queryVolumes;

            public InputGroundTrackerMode trackerMode;

            [Tooltip("Automatically look up named layers instead of using a mask by putting the names here")]
            public List<string> layerNames;
        }
    }
}