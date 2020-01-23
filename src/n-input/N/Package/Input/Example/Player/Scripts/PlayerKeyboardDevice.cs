using N.Package.Input;
using UnityEngine;

namespace Content.Player.Scripts
{
    public class PlayerKeyboardDevice : InputDevice
    {
        public PlayerState state;

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
            if (typeof(TState) != typeof(PlayerState)) return null;
            return state as TState;
        } 

        public void Update()
        {
            if (!_active) return;

            if (Input.GetKey(up))
            {
                state.z = 1;
            }
            else if (Input.GetKeyDown(down))
            {
                state.z = -1;
            }
            else if (Input.GetKeyUp(up))
            {
                state.z = 0;
            }
            else if (Input.GetKeyUp(down))
            {
                state.z = 0;
            }

            if (Input.GetKeyDown(left))
            {
                state.x = -1;
            }
            else if (Input.GetKeyDown(right))
            {
                state.x = 1;
            }
            else if (Input.GetKeyUp(left))
            {
                state.x = 0;
            }
            else if (Input.GetKeyUp(right))
            {
                state.x = 0;
            }

            if (Input.GetKeyDown(jump))
            {
                _jump = true;
            }
            else if (Input.GetKeyUp(jump))
            {
                _jump = false;
            }

            state.y = _jump ? 1 : 0;
        }
    }
}