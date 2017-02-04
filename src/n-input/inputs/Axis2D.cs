using N.Package.Input;
using UnityEngine;

namespace N.Packages.Input
{
    public abstract class Axis2D : IInput
    {
        private int _id;
        private int _deviceId;

        public Axis2D(int inputId, int deviceId)
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

        public Vector2 GetValue()
        {
            return Value();
        }

        // Implement the axis data source here.
        protected abstract Vector2 Value();
    }
}