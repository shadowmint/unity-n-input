using System;
using System.Collections.Generic;
using N.Packages.Input;
using UE = UnityEngine;

namespace N.Package.Input.Controllers
{
    public abstract class BaseController : IDevice
    {
        private readonly string _joystickDeviceName;
        private readonly int _deviceId;
        private DeviceButtons _buttons;

        protected readonly List<string> AxisNames = new List<string>();
        private readonly List<Axis2D> _axis = new List<Axis2D>();
        private bool _initialized;

        private const string AxisIdTemplate = "Joy{0}Axis{1}";
        private const int MinInputId = 1;
        private const int MaxInputId = 100;

        protected readonly int JoystickId;

        protected BaseController(int joystickId, string deviceName)
        {
            JoystickId = joystickId;
            _joystickDeviceName = deviceName;
            _deviceId = Devices.InputId;
        }

        public int DeviceId
        {
            get { return _deviceId; }
        }

        public string DeviceName
        {
            get { return _joystickDeviceName; }
        }

        public IEnumerable<IInput> Inputs
        {
            get
            {
                Initialize();
                yield return _buttons;
                foreach (var axis in _axis)
                {
                    yield return axis;
                }
            }
        }

        private void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            DeviceSpecificAxis();
            _buttons = DeviceSpecificButtonMap(Devices.InputId, _deviceId);
        }

        protected void AddAxis(Axis2D axis)
        {
            _axis.Add(axis);
        }

        // Implement in subclasses
        protected abstract DeviceButtons DeviceSpecificButtonMap(int inputId, int deviceId);

        // Implement in subclasses
        protected abstract void DeviceSpecificAxis();
    }
}