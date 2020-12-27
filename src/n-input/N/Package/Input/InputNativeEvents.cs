using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace N.Package.Input
{
    public static class InputNativeEvents
    {
        public static Vector2 CursorPosition()
        {
            if (Mouse.current == null) return Vector2.zero;
            var positionBinding = Mouse.current.position;
            return new Vector2(positionBinding.x.ReadValue(), positionBinding.y.ReadValue());
        }

        public static bool CursorDown(CursorButtonId buttonId)
        {
            if (Mouse.current == null) return false;
            var cursorBinding = Mouse.current;
            switch (buttonId)
            {
                case CursorButtonId.Left:
                    return cursorBinding.leftButton.isPressed;

                case CursorButtonId.Right:
                    return cursorBinding.rightButton.isPressed;

                case CursorButtonId.Middle:
                    return cursorBinding.middleButton.isPressed;

                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonId), buttonId, null);
            }
        }
        
        public static bool CursorClicked(CursorButtonId buttonId)
        {
            if (Mouse.current == null) return false;
            var cursorBinding = Mouse.current;
            switch (buttonId)
            {
                case CursorButtonId.Left:
                    return cursorBinding.leftButton.wasReleasedThisFrame;

                case CursorButtonId.Right:
                    return cursorBinding.rightButton.wasReleasedThisFrame;

                case CursorButtonId.Middle:
                    return cursorBinding.middleButton.wasReleasedThisFrame;

                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonId), buttonId, null);
            }
        }
        
        public static bool CursorUp(CursorButtonId buttonId)
        {
            if (Mouse.current == null) return false;
            var cursorBinding = Mouse.current;
            switch (buttonId)
            {
                case CursorButtonId.Left:
                    return !cursorBinding.leftButton.isPressed;

                case CursorButtonId.Right:
                    return !cursorBinding.rightButton.isPressed;

                case CursorButtonId.Middle:
                    return !cursorBinding.middleButton.isPressed;

                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonId), buttonId, null);
            }
        }

        public enum CursorButtonId
        {
            Left,
            Right,
            Middle
        }
    }
}