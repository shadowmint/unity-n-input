using N.Package.Input.Example.Infrastructure;
using N.Package.Input.Tooling;
using UnityEngine;

namespace N.Package.Input.Example
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(InputGroundTracker))]
    public class InputExampleActor : InputActor<InputExamplePlayerInput>
    {
        public InputExampleMovement movement;

        protected override void UpdateFromInput(InputExamplePlayerInput input)
        {
            movement.UpdateFromInput(input, transform);
        }
    }
}
