using UnityEngine;

namespace N.Packages.Input
{
    public class InputAxis2D : Axis2D
    {
        private readonly string _axisIdX;
        private readonly string _axisIdY;

        public InputAxis2D(string axisIdX, string axisIdY, int inputId, int deviceId) : base(inputId, deviceId)
        {
            _axisIdX = axisIdX;
            _axisIdY = axisIdY;
        }

        protected override Vector2 Value()
        {
            return new Vector2(UnityEngine.Input.GetAxis(_axisIdX), UnityEngine.Input.GetAxis(_axisIdY));
        }
    }
}