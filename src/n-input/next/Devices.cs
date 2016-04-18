using System.Collections.Generic;

namespace N.Package.Input.Next
{
    /// High level device API
    public class Devices
    {
        /// Return the next id for an input device
        private static int inputId = 0;
        public static int InputId
        {
            get
            {
                inputId += 1;
                return inputId;
            }
        }

        /// Return the keyboard device
        private static Keyboard keyboard;
        public static Keyboard Keyboard
        {
            get
            {
                if (keyboard == null)
                {
                    keyboard = new Keyboard();
                }
                return keyboard;
            }
        }

        /// Return the mouse device
        private static Mouse mouse;
        public static Mouse Mouse
        {
            get
            {
                if (mouse == null)
                {
                    mouse = new Mouse();
                }
                return mouse;
            }
        }

        /// Clear all devices
        /// eg. For new level is loaded
        /// Notice this does not clear Inputs instances; use should use
        /// Inputs.Clear() to clear before loading level, not this.
        public static void Clear()
        {
            mouse = null;
            keyboard = null;
            inputId = 0;
        }
    }
}
