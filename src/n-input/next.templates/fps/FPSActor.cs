using UnityEngine;

namespace N.Package.Input.Next.Templates.FPS
{
    [RequireComponent(typeof(Rigidbody))]
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

        /// The rigid body
        private Rigidbody body;

        /// The current motion state
        public FPSMotionState motion;

        /// Head state of this actor
        private FPSHeadState headState;

        public void Start()
        {
            headState = new FPSHeadState(gameObject, transform.up);
            body = GetComponent<Rigidbody>();
        }

        /// Execute an action on this actor
        public override void Trigger<TAction>(TAction action)
        {
            LookAt(action as FPSLookAtEvent);
            Move(action as FPSMoveEvent);
        }

        /// Deal with look actions
        public void LookAt(FPSLookAtEvent data)
        {
            if (data != null)
            {
                var y = Mathf.Clamp(data.point.y * -90f, -maxLookUpDown, maxLookUpDown);
                var x = Mathf.Clamp(data.point.x * 90f, -maxLookLeftRight, maxLookLeftRight);
                head.SetRotation(new Vector3(y, x, 0f));
                //transform.rotation = headState.rotation;
            }
        }

        /// Deal with move actions
        public void Move(FPSMoveEvent data)
        {
            if (data != null)
            {
                // Forwards backwards
                if ((data.code == FPSMotion.FORWARDS) && (data.active))
                {
                    motion.motion = FPSMotion.FORWARDS;
                }
                else if ((data.code == FPSMotion.FORWARDS) && (!data.active) && (motion.motion == FPSMotion.FORWARDS))
                {
                    motion.motion = FPSMotion.IDLE;
                }
                else if ((data.code == FPSMotion.BACKWARDS) && (data.active))
                {
                    motion.motion = FPSMotion.BACKWARDS;
                }
                else if ((data.code == FPSMotion.BACKWARDS) && (!data.active) && (motion.motion == FPSMotion.BACKWARDS))
                {
                    motion.motion = FPSMotion.IDLE;
                }

                // Left / right
                if ((data.code == FPSMotion.RIGHT) && (data.active))
                {
                    motion.lateralMotion = FPSMotion.RIGHT;
                }
                else if ((data.code == FPSMotion.RIGHT) && (!data.active) && (motion.lateralMotion == FPSMotion.RIGHT))
                {
                    motion.lateralMotion = FPSMotion.IDLE;
                }
                else if ((data.code == FPSMotion.LEFT) && (data.active))
                {
                    motion.lateralMotion = FPSMotion.LEFT;
                }
                else if ((data.code == FPSMotion.LEFT) && (!data.active) && (motion.lateralMotion == FPSMotion.LEFT))
                {
                    motion.lateralMotion = FPSMotion.IDLE;
                }

                // Turn left / right
                if ((data.code == FPSMotion.TURN_RIGHT) && (data.active))
                {
                    motion.turn = FPSMotion.TURN_RIGHT;
                }
                else if ((data.code == FPSMotion.TURN_RIGHT) && (!data.active) && (motion.turn == FPSMotion.TURN_RIGHT))
                {
                    motion.turn = FPSMotion.IDLE;
                }
                else if ((data.code == FPSMotion.TURN_LEFT) && (data.active))
                {
                    motion.turn = FPSMotion.TURN_LEFT;
                }
                else if ((data.code == FPSMotion.TURN_LEFT) && (!data.active) && (motion.turn == FPSMotion.TURN_LEFT))
                {
                    motion.turn = FPSMotion.IDLE;
                }
            }
        }

        public void Update()
        {
            this.TriggerPending<FPSAction>();
            motion.Update(body);
        }
    }

    /// Motion states
    public enum FPSMotion
    {
        IDLE,
        TURN_LEFT,
        TURN_RIGHT,
        FORWARDS,
        BACKWARDS,
        LEFT,
        RIGHT
    }

    /// Motion state
    [System.Serializable]
    public class FPSMotionState
    {
        public FPSMotion turn;
        public FPSMotion motion;
        public FPSMotion lateralMotion;

        [Tooltip("The maximum linear speed to allow acceleration to occur in")]
        public float maxSpeed;

        [Tooltip("The maximum angular speed to allow acceleration to occur in")]
        public float maxTurnSpeed;

        [Tooltip("The force to apply to generate linear acceleration")]
        public float linearForce;

        [Tooltip("The force to apply to generate angular acceleration")]
        public float angularForce;

        public void Update(Rigidbody body)
        {
            if (body != null)
            {
                // Moving
                var velocity = body.velocity;

                // Forwards / backgrounds
                var linearVelocity = Vector3.Project(velocity, body.transform.forward);
                if (motion == FPSMotion.FORWARDS)
                {
                    if (linearVelocity.magnitude < maxSpeed)
                    {
                        var force = body.transform.forward * Time.deltaTime * linearForce * body.mass;
                        body.AddForce(force);
                    }
                }
                else if (motion == FPSMotion.BACKWARDS)
                {
                    if ((-1f * linearVelocity.magnitude) > (-1f * maxSpeed))
                    {
                        var force = -1f * body.transform.forward * Time.deltaTime * linearForce * body.mass;
                        body.AddForce(force);
                    }
                }

                // Left / right
                linearVelocity = Vector3.Project(velocity, body.transform.right);
                if (lateralMotion == FPSMotion.RIGHT)
                {
                    if (linearVelocity.magnitude < maxSpeed)
                    {
                        var force = body.transform.right * Time.deltaTime * linearForce * body.mass;
                        body.AddForce(force);
                    }
                }
                else if (lateralMotion == FPSMotion.LEFT)
                {
                    if ((-1f * linearVelocity.magnitude) > (-1f * maxSpeed))
                    {
                        var force = -1f * body.transform.right * Time.deltaTime * linearForce * body.mass;
                        body.AddForce(force);
                    }
                }

                // Turning
                var angular = body.angularVelocity;
                if (turn == FPSMotion.TURN_RIGHT)
                {
                    if (angular.magnitude < maxTurnSpeed)
                    {
                        var force = body.transform.up * Time.deltaTime * angularForce * body.mass;
                        body.AddTorque(force);
                    }
                }
                else if (turn == FPSMotion.TURN_LEFT)
                {
                    if ((-1f * angular.magnitude) > (-1f * maxTurnSpeed))
                    {
                        var force = -1f * body.transform.up * Time.deltaTime * angularForce * body.mass;
                        body.AddTorque(force);
                    }
                }
                else if (turn == FPSMotion.IDLE)
                {
                    body.angularVelocity = Vector3.zero; 
                }
            }
        }
    }

    /// Head tracking helper
    public class FPSHeadState
    {
        /// Global up axis
        public Vector3 up;

        /// Current rotation state
        public Quaternion rotation;

        /// Last rotation value
        public Vector3 lastKnownRotation;

        public FPSHeadState(GameObject initialState, Vector3 up)
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
