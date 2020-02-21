using UnityEngine;

namespace N.Package.Input.Example.Infrastructure
{
    public class InputExamplePlayerKeys : InputDevice
    {
        public InputExamplePlayerInput state;

        public KeyCode up = KeyCode.UpArrow;
        public KeyCode down = KeyCode.DownArrow;
        public KeyCode left = KeyCode.LeftArrow;
        public KeyCode right = KeyCode.RightArrow;
        public KeyCode jump = KeyCode.Space;

        private bool _active;
        private bool _jump;

        public override void IsConnected(bool connected)
        {
            _active = connected;
        }

        public override TState GetState<TState>()
        {
            if (typeof(TState) != typeof(InputExamplePlayerInput)) return null;
            return state as TState;
        }

        public void Update()
        {
            if (!_active) return;

            if (UnityEngine.Input.GetKey(up))
            {
                state.z = 1;
            }
            else if (UnityEngine.Input.GetKeyDown(down))
            {
                state.z = -1;
            }
            else if (UnityEngine.Input.GetKeyUp(up))
            {
                state.z = 0;
            }
            else if (UnityEngine.Input.GetKeyUp(down))
            {
                state.z = 0;
            }

            if (UnityEngine.Input.GetKeyDown(left))
            {
                state.x = -1;
            }
            else if (UnityEngine.Input.GetKeyDown(right))
            {
                state.x = 1;
            }
            else if (UnityEngine.Input.GetKeyUp(left))
            {
                state.x = 0;
            }
            else if (UnityEngine.Input.GetKeyUp(right))
            {
                state.x = 0;
            }

            if (UnityEngine.Input.GetKeyDown(jump))
            {
                _jump = true;
            }
            else if (UnityEngine.Input.GetKeyUp(jump))
            {
                _jump = false;
            }

            state.y = _jump ? 1 : 0;
        }
    }
}