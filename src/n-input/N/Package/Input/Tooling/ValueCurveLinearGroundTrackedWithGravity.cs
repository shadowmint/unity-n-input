using UnityEngine;

namespace N.Package.Input.Tooling
{
    /// <summary>
    /// Allow linear velocity only while grounded and add gravity.
    /// </summary>
    [System.Serializable]
    public class ValueCurveLinearGroundTrackedWithGravity : ValueCurveLinearGroundTracked
    {
        public float gravity;

        protected override float Next(float step)
        {
            var nextValue = base.Next(step);
            if (groundTracker.state.grounded && nextValue < 0)
            {
                return 0;
            }
            
            nextValue -= gravity * Time.deltaTime;
            return nextValue;
        }
    }
}