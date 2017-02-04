using System.Collections.Generic;
using System.Linq;
using N.Package.Input;
using UnityEngine;

namespace N.Packages.Input
{
    /// DeviceButtons is like the generic Buttons input, but abstracts over the actual
    /// button ids to provide a consistent high level api to access buttons on.
    public abstract class DeviceButtons : IInput
    {
        private readonly int _id;
        private readonly int _deviceId;

        protected DeviceButtons(int inputId, int deviceId)
        {
            _id = inputId;
            _deviceId = deviceId;
        }

        public int DeviceId
        {
            get { return _deviceId; }
        }

        public int Id
        {
            get { return _id; }
        }

        public bool Down<T>(T key)
        {
            KeyCode code;
            if (MapCode(key, out code))
            {
                return UnityEngine.Input.GetKeyDown(code);
            }
            _.Error("Key {0} is not supported by DeviceButtons input id {1}::{2}", key, _deviceId, _id);
            return false;
        }

        public bool Up<T>(T key)
        {
            KeyCode code;
            if (MapCode(key, out code))
            {
                return UnityEngine.Input.GetKeyUp(code);
            }
            _.Error("Key {0} is not supported by DeviceButtons input id {1}::{2}", key, _deviceId, _id);
            return false;
        }

        public IEnumerable<KeyCode> AllDown()
        {
            return DeviceKeyCodes().Where(UnityEngine.Input.GetKeyDown);
        }

        protected abstract bool MapCode<T>(T value, out KeyCode code);

        protected abstract IEnumerable<KeyCode> DeviceKeyCodes();
    }
}