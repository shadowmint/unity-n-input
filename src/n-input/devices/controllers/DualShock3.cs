using System.Collections.Generic;
using N.Packages.Input;
using UnityEngine;

namespace N.Package.Input.Controllers
{
    public class DualShock3 : BaseController
    {
        private const string AxisX1 = "Joy1Axis1";
        private const string AxisY1 = "Joy1Axis2";
        private const string AxisX2 = "Joy1Axis3";
        private const string AxisY2 = "Joy1Axis4";

        public static bool Matches(string id)
        {
            return id == "Sony PLAYSTATION(R)3 Controller";
        }

        public DualShock3(int joystickId, string deviceName) : base(joystickId, deviceName)
        {
        }

        protected override DeviceButtons DeviceSpecificButtonMap(int inputId, int deviceId)
        {
            return new DualShock3Buttons(JoystickId, inputId, deviceId);
        }

        protected override void DeviceSpecificAxis()
        {
            AddAxis(new InputAxis2D(AxisX1, AxisY1, Devices.InputId, DeviceId));
            AddAxis(new InputAxis2D(AxisX2, AxisY2, Devices.InputId, DeviceId));
        }
    }

    public class DualShock3Buttons : BaseDeviceButtons
    {
        private readonly Dictionary<DualShock3Keys, KeyCode> _codeMap = new Dictionary<DualShock3Keys, KeyCode>();

        public DualShock3Buttons(int joystickId, int inputId, int deviceId) : base(joystickId, inputId, deviceId)
        {
            _codeMap.Add(DualShock3Keys.Select, CodeFor(0));
            _codeMap.Add(DualShock3Keys.LeftStick, CodeFor(1));
            _codeMap.Add(DualShock3Keys.RightStick, CodeFor(2));
            _codeMap.Add(DualShock3Keys.Start, CodeFor(3));
            _codeMap.Add(DualShock3Keys.UpDPad, CodeFor(4));
            _codeMap.Add(DualShock3Keys.RightDPad, CodeFor(5));
            _codeMap.Add(DualShock3Keys.DownDPad, CodeFor(6));
            _codeMap.Add(DualShock3Keys.LeftDPad, CodeFor(7));
            _codeMap.Add(DualShock3Keys.L2, CodeFor(8));
            _codeMap.Add(DualShock3Keys.R2, CodeFor(9));
            _codeMap.Add(DualShock3Keys.L1, CodeFor(10));
            _codeMap.Add(DualShock3Keys.R1, CodeFor(11));
            _codeMap.Add(DualShock3Keys.Triangle, CodeFor(12));
            _codeMap.Add(DualShock3Keys.Circle, CodeFor(13));
            _codeMap.Add(DualShock3Keys.Cross, CodeFor(14));
            _codeMap.Add(DualShock3Keys.Square, CodeFor(15));
            _codeMap.Add(DualShock3Keys.PsButtom, CodeFor(16));
        }

        protected override bool MapCode<T>(T value, out KeyCode code)
        {
            code = KeyCode.Break;
            if (typeof(T) != typeof(DualShock3Keys)) return false;
            var button = (DualShock3Keys) (object) value;
            if (!_codeMap.ContainsKey(button)) return false;
            code = _codeMap[button];
            return true;
        }
    }

    public enum DualShock3Keys
    {
        Triangle,
        Square,
        Circle,
        Cross,
        LeftDPad,
        RightDPad,
        UpDPad,
        DownDPad,
        L1,
        L2,
        R1,
        R2,
        LeftStick,
        RightStick,
        Start,
        Select,
        PsButtom
    }
}