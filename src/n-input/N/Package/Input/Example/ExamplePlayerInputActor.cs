using N.Package.Input.Tooling;
using UnityEngine;

namespace N.Package.Input.Example
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(InputGroundTracker))]
    public class ExamplePlayerInputActor : InputActor<ExamplePlayerInputState>
    {
        public ExamplePlayerInputMovement movement;

        protected override void UpdateFromInput(ExamplePlayerInputState input)
        {
            movement.UpdateFromInput(input, transform);
        }
    }
}