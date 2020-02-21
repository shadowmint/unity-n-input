using UnityEngine;

namespace N.Package.Input.Tooling
{
    [System.Serializable]
    public class ValueCurveLinear : ValueCurve
    {
        public float acceleration;

        public float dampening;

        protected override float Next(float step)
        {
            var delta = acceleration * step * Time.deltaTime;
            if (Mathf.Abs(delta) > 0)
            {
                return value + delta;
            }
            
            delta = dampening * Time.deltaTime;
            if (delta > Mathf.Abs(value))
            {
                delta = value;
            }
            
            return value - Mathf.Sign(value) * delta;
        }
    }
}