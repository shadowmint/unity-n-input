using N.Package.Input;
using N.Package.Input.Infrastructure;
using UnityEngine;

namespace Content.Player.Scripts
{
    public class PlayerActor : InputActor<PlayerState>
    {
        public float speed;
        public float speedJump;
        public float maxDelta;
        public float maxJump;
        
        private Rigidbody _body;

        public void Start()
        {
            _body = GetComponent<Rigidbody>();
        }

        protected override void UpdateFromInput(PlayerState state)
        {
            var t = transform;
            var x = state.x * speed * Vector3.right;
            var z = state.z * speed * Vector3.forward;
            
            var y = state.y * speedJump * Vector3.up;
            if (_body.velocity.y > maxJump)
            {
                y = Vector3.zero;
            }

            var v = x + z + y;
            var f = Mathf.Clamp(v.magnitude, 0f, maxDelta * Time.deltaTime) * _body.mass * v.normalized;
            
            if (y.magnitude > 0)
            {
                f -= Physics.gravity;
            }

            _body.AddForce(f);
        }
    }
}