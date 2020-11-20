using N.Package.Input.Tooling;
using UnityEngine;

namespace N.Package.Input.Example
{
    [System.Serializable]
    public class ExamplePlayerInputMovement
    {
        [Tooltip("Face direction if moving this fast in this direction")]
        public float changeDirectionThreshold = 1f;

        public ValueCurveLinearGroundTracked x;
        public ValueCurveLinearGroundTracked z;
        public ValueCurveLinearGroundTrackedWithGravity y;
        public GameObject referenceObject;
        public bool invertReference = false;
        private ClampedVector _clamped = new ClampedVector();
        private Rigidbody _body;
        private InputGroundTracker _groundTracker;

        public void UpdateFromInput(ExamplePlayerInputState state, Transform player)
        {
            if (_body == null)
            {
                _body = player.GetComponent<Rigidbody>();
            }

            if (_groundTracker == null)
            {
                _groundTracker = player.GetComponent<InputGroundTracker>();
                x.groundTracker = _groundTracker;
                y.groundTracker = _groundTracker;
                z.groundTracker = _groundTracker;
            }

            if (referenceObject == null)
            {
                if (Camera.main == null)
                {
                    Debug.Log("No global reference object set! It should be set for movement!");
                    referenceObject = player.gameObject;
                }
                else
                {
                    referenceObject = Camera.main.gameObject;    
                }
            }

            // Look in the right direction
            var realXz = Vector3.ProjectOnPlane(_body.velocity, Vector3.up);
            if (realXz.magnitude > 1f)
            {
                _body.rotation = Quaternion.LookRotation(realXz);
            }

            // Update state
            var t = player;
            x.Update(state.move.x);
            z.Update(state.move.y);
            y.Update(state.jump ? 1f : 0f);

            // Calculate new desired velocity
            var forward = Vector3.ProjectOnPlane(referenceObject.transform.forward, Vector3.up);
            var right = Vector3.ProjectOnPlane(referenceObject.transform.right, Vector3.up);
            var xVel = x.InDirection(right * (invertReference ? -1 : 1));
            var zVel = z.InDirection(forward * (invertReference ? -1 : 1));

            // Clamp XZ so x + z doesn't make you run faster
            _clamped.MaxMagnitude = x.minMax.y;
            _clamped.MinMagnitude = x.minMax.x;
            var xzVel = _clamped.Clamp(xVel + zVel);

            // Apply jump & gravity
            var yVal = y.InDirection(Vector3.up);

            // Apply
            var targetVelocity = xzVel + yVal;
            _body.velocity = targetVelocity;
        }
    }
}