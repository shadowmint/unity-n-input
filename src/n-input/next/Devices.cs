using System.Collections.Generic;

namespace N.Package.Input.Next
{
    /// High level device API
    /// Use this, or construct a device manually.
    public class Devices
    {
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
    }
}
