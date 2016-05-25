using UnityEngine;

namespace N.Package.Input.Next.Templates.FPS
{
    public class FPSActor : Actor
    {
        [Tooltip("Maximum left right look extents")]
        [Range(45f, 110f)]
        public float maxLookLeftRight = 90f;

        [Tooltip("Maximum up down look extents")]
        [Range(45f, 110f)]
        public float maxLookUpDown = 90f;

        [Tooltip("The speed this actor moves at")]
        public float speed = 1f;

        [Tooltip("The object to use as this avatar's head")]
        public GameObject head;

        /// The current motion state
        public MotionState motion;

        /// Head state of this actor
        private HeadState headState;

        public void Start()
        {
            headState = new HeadState(gameObject, transform.up);
        }

        /// Execute an action on this actor
        public override void Trigger<TAction>(TAction action)
        {
            LookAt(action as FPSLookAtEvent);
        }

        /// Deal with look actions
        public void LookAt(FPSLookAtEvent data)
        {
            if (data != null)
            {
                var y = Mathf.Clamp(data.point.y * -90f, -maxLookUpDown, maxLookUpDown);
                var x = Mathf.Clamp(data.point.x * 90f, -maxLookLeftRight, maxLookLeftRight);
                head.SetRotation(new Vector3(y, x, 0f));
                transform.rotation = headState.rotation;
            }
        }

        public void Update()
        {
            this.TriggerPending<FPSAction>();
        }
    }

    /// Motion states
    public enum MotionStateType
    {
        IDLE,
        TURNING_LEFT,
        TURNING_RIGHT,
        MOVE_FORWARDS,
        MOVE_BACKWARDS,
        MOVE_LEFT,
        MOVE_RIGHT
    }

    /// Motion state
    [System.Serializable]
    public class MotionState
    {
        public MotionStateType turn;
        public MotionStateType motion;
        public MotionStateType lateralMotion;
    }

    /// Head tracking helper
    public class HeadState
    {
        /// Global up axis
        public Vector3 up;

        /// Current rotation state
        public Quaternion rotation;

        /// Last rotation value
        public Vector3 lastKnownRotation;

        public HeadState(GameObject initialState, Vector3 up)
        {
            this.up = up;
            rotation = initialState.transform.rotation;
            lastKnownRotation = rotation.eulerAngles;
        }

        /// Set the absolute rotation on this target
        public void SetRotation(Vector3 value)
        {
            var delta = lastKnownRotation - value;
            if (delta.magnitude > 0f)
            {
                lastKnownRotation = value;
                Rotate(delta);
            }
        }

        /// Rotate somewhat in some direction with Euler angles
        public void Rotate(Vector3 change)
        {
            var up = Quaternion.AngleAxis(change.x, Right);
            var left = Quaternion.AngleAxis(change.y, this.up);
            var tilt = Quaternion.AngleAxis(change.z, Forward);
            rotation = left * up * tilt * rotation;
        }

        /// Housekeeping
        private float x { get { return rotation.x; } }
        private float y { get { return rotation.y; } }
        private float z { get { return rotation.z; } }
        private float w { get { return rotation.w; } }
        private Vector3 Forward { get { return new Vector3(2 * (x * z + w * y), 2 * (y * x - w * x), 1 - 2 * (x * x + y * y)); } }
        private Vector3 Up { get { return new Vector3(2 * (x * y - w * z), 1 - 2 * (x * x + z * z), 2 * (y * z + w * x)); } }
        private Vector3 Right { get { return new Vector3(1 - 2 * (y * y + z * z), 2 * (x * y + w * z), 2 * (x * z - w * y)); } }
    }
}
