using System.Collections.Generic;
using N.Package.Input.Next.Helpers;
using UnityEngine;

namespace N.Package.Input.Next
{
    public class Mouse : IDevice
    {
        /// The actual input cursor
        private Cursor2 cursor;

        /// Buttons
        private Buttons buttons;

        /// A hook for a 3d intersecting cursor
        private IList<Collider3> rays = new List<Collider3>();

        public Mouse()
        {
            cursor = new Cursor2(Devices.InputId);
            buttons = new Buttons(Devices.InputId);
        }

        /// Add a 3d intersection cursor to this mouse
        /// @param distance The raycast distance
        /// @param camera The camera to use, to null for the default camera.
        /// @param layerMask The raycast layer mask, defaults to the default mask.
        /// Returns the id of the add input.
        public int EnableRaycast(float distance, Camera camera = null, int layerMask = Physics.DefaultRaycastLayers)
        {
            var factory = new CameraRaycastFactory()
            {
                Distance = distance,
                LayerMask = layerMask,
                camera = camera ?? Camera.main
            };
            var collider = new Collider3(Devices.InputId, factory);
            rays.Add(collider);
            return collider.Id;
        }

        /// Add a 2d intersection cursor to this mouse
        /// @param distance The raycast distance
        /// @param camera The camera to use, to null for the default camera.
        /// @param layerMask The raycast layer mask, defaults to the default mask.
        /// Returns the id of the add input.
        public int EnableRaycast2D(float distance, Camera camera = null, int layerMask = Physics.DefaultRaycastLayers)
        {
            var factory = new CameraRaycastFactory()
            {
                Distance = distance,
                LayerMask = layerMask,
                camera = camera ?? Camera.main
            };
            var collider = new Collider3(Devices.InputId, factory, ColliderType.Raycast2D);
            rays.Add(collider);
            return collider.Id;
        }

        /// The set of inputs on this device
        public IEnumerable<IInput> Inputs
        {
            get
            {
                // Actual mouse position
                yield return cursor;

                // Buttons
                yield return buttons;

                // Colliders
                foreach (var collider in rays)
                {
                    var factory = (collider.Factory as CameraRaycastFactory);
                    factory.Update(cursor);
                    yield return collider;
                }
            }
        }
    }
}
