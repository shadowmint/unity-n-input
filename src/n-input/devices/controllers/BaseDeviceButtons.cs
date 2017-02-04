using System;
using System.Collections.Generic;
using N.Packages.Input;
using UnityEngine;

namespace N.Package.Input.Controllers
{
    public abstract class BaseDeviceButtons : DeviceButtons
    {
        private const int MinInputId = 0;
        private const int MaxInputId = 100;

        private const string JoystickButtonIdTemplate = "Joystick{0}Button{1}";

        protected readonly int JoystickId;

        protected BaseDeviceButtons(int joystickId, int inputId, int deviceId) : base(inputId, deviceId)
        {
            JoystickId = joystickId;
        }

        protected override IEnumerable<KeyCode> DeviceKeyCodes()
        {
            for (var i = MinInputId; i < MaxInputId; i++)
            {
                bool match;
                var code = CodeFor(i, out match);
                if (match)
                {
                    yield return code;
                }
            }
        }

        protected KeyCode CodeFor(int buttonId)
        {
            bool match;
            return CodeFor(buttonId, out match);
        }

        protected KeyCode CodeFor(int buttonId, out bool match)
        {
            match = false;
            var code = KeyCode.Break;
            try
            {
                var name = string.Format(JoystickButtonIdTemplate, JoystickId + 1, buttonId);
                code = (KeyCode) Enum.Parse(typeof(KeyCode), name);
                match = true;
            }
            catch (OverflowException)
            {
            }
            catch (ArgumentNullException)
            {
            }
            catch (ArgumentException)
            {
            }
            return code;
        }
    }
}