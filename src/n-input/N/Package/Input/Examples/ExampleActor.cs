using System;
using UnityEngine;

namespace N.Package.Input.Examples
{
    public class ExampleActor : MonoBehaviour
    {
        public ExamplePlayer.State state;

        public float speed = 5;

        public float turnSpeed = 5;

        public void Update()
        {
            var forward = transform.forward;
            var right = transform.right;

            var df = forward * speed * state.movement.y * Time.deltaTime;
            var dr = right * speed * state.movement.x * Time.deltaTime;
            transform.position += df + dr;

            var dturn = turnSpeed * Time.deltaTime * state.look.x;
            transform.RotateAround(transform.position, Vector3.up, dturn);
        }
    }
}