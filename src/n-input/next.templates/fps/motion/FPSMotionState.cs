using UnityEngine;

namespace N.Package.Input.Next.Templates.FPS
{
    /// Motion state
    [System.Serializable]
    public class FPSMotionState
    {
        public FPSMotion turn;
        public FPSMotion motion;
        public FPSMotion lateralMotion;
        public FPSMotion verticalMotion;

        [Tooltip("The maximum linear speed to allow acceleration to occur in")]
        public float maxSpeed;

        [Tooltip("The maximum angular speed to allow acceleration to occur in")]
        public float maxTurnSpeed;

        [Tooltip("The force to apply to generate linear acceleration")]
        public float linearForce;

        [Tooltip("The jump force to apply to generate upwards acceleration")]
        public float jumpForce;

        [Tooltip("The force to apply to generate angular acceleration")]
        public float angularForce;

        [Tooltip("The current 'forward' vecotr")]
        public Vector3 forward;

        [Tooltip("The current 'right' vecotr")]
        public Vector3 right;

        [Tooltip("Is this actor currently on solid ground?")]
        public bool isFalling = false;

        /// Never allow falls to be shorter than this (ie. bounces)
        private float minAirTime = 0.5f;

        /// How long have we been falling?
        private float airTime = 0f;

        [Tooltip("Jump detect distance")]
        public float fallingRaycastDistance = 1f;

        public void SyncMotionToLook(GameObject head, Rigidbody body)
        {
            var target = head.transform.position + head.transform.forward * 2f;
            target.y = head.transform.position.y;
            forward = (target - head.transform.position).normalized;
            right = Vector3.Cross(body.transform.up, forward);
        }

        public void UpdateFallingState(Rigidbody body)
        {
            var fall = !Physics.Raycast(body.transform.position, -body.transform.up, fallingRaycastDistance);
            if (fall)
            {
                if (!isFalling)
                {
                    airTime = 0f;
                }
                isFalling = true;
            }
            else
            {
                airTime += Time.deltaTime;
                if (airTime > minAirTime)
                {
                    airTime = 0f;
                    isFalling = false;
                }
            }
        }

        public void Update(Rigidbody body)
        {
            if (body != null)
            {
                // Moving
                var velocity = body.velocity;

                // Jumping
                UpdateFallingState(body);
                if ((verticalMotion == FPSMotion.JUMP) && (!isFalling))
                {
                    var force = body.transform.up * jumpForce * body.mass;
                    body.AddForce(force);
                    isFalling = true;
                }

                // Less movement while falling
                var forceMulti = isFalling ? 0.5f : 1.0f;

                // Forwards / backgrounds
                var linearVelocity = Vector3.Project(velocity, body.transform.forward);
                if (motion == FPSMotion.FORWARDS)
                {
                    if (linearVelocity.magnitude < maxSpeed)
                    {
                        var force = forceMulti * forward * Time.deltaTime * linearForce * body.mass;
                        body.AddForce(force);
                    }
                }
                else if (motion == FPSMotion.BACKWARDS)
                {
                    if ((-1f * linearVelocity.magnitude) > (-1f * maxSpeed))
                    {
                        var force = -forceMulti * forward * Time.deltaTime * linearForce * body.mass;
                        body.AddForce(force);
                    }
                }

                // Left / right
                linearVelocity = Vector3.Project(velocity, body.transform.right);
                if (lateralMotion == FPSMotion.RIGHT)
                {
                    if (linearVelocity.magnitude < maxSpeed)
                    {
                        var force = forceMulti * right * Time.deltaTime * linearForce * body.mass;
                        body.AddForce(force);
                    }
                }
                else if (lateralMotion == FPSMotion.LEFT)
                {
                    if ((-1f * linearVelocity.magnitude) > (-1f * maxSpeed))
                    {
                        var force = -forceMulti * right * Time.deltaTime * linearForce * body.mass;
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
}
