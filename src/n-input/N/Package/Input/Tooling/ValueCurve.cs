using UnityEngine;

namespace N.Package.Input.Tooling
{
    [System.Serializable]
    public abstract class ValueCurve
    {
        public float value;

        public Vector2 minMax;

        /// <summary>
        /// Step the current value along the curve with the increment marked by value. 
        /// </summary>
        public void Update(float step)
        {
            value = Mathf.Clamp(Next(step), minMax.x, minMax.y);
        }

        /// <summary>
        /// Project the value into a Vector3.
        /// </summary>
        public Vector3 InDirection(Vector3 direction, bool enableValue = true)
        {
            if (!enableValue) return Vector3.zero;
            return direction.normalized * value;
        }

        /// <summary>
        /// Project the value into a Vector2.
        /// </summary>
        public Vector2 InDirection(Vector2 direction, bool enableValue = true)
        {
            if (!enableValue) return Vector2.zero;
            return direction.normalized * value;
        }

        /// <summary>
        /// Step the current value along the curve with the increment marked by value. 
        /// </summary>
        protected abstract float Next(float step);
    }
}