using UnityEngine;
using UnityEngine.InputSystem;

namespace N.Package.Input.Example
{
    public class ExamplePlayerInputBinding : InputDevice
    {
        public ExamplePlayerInputState state;

        private bool _active;

        public override void IsConnected(bool connected)
        {
            _active = connected;
        }

        public override TState GetState<TState>()
        {
            if (typeof(TState) != typeof(ExamplePlayerInputState)) return null;
            return state as TState;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            state.move = value;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            state.look = value;
        }
    }
}