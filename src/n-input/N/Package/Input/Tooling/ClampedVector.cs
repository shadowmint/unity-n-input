using UnityEngine;

namespace N.Package.Input.Tooling
{
    public class ClampedVector
    {
        public float MinMagnitude { get; set; }
        public float MaxMagnitude { get; set; }

        public Vector3 Clamp(Vector3 value)
        {
            var direction = value.normalized;
            if (value.magnitude < MinMagnitude)
            {
                return direction * MinMagnitude;
            }
            else if (value.magnitude > MaxMagnitude)
            {
                return direction * MaxMagnitude;
            }

            return value;
        }
    }
}