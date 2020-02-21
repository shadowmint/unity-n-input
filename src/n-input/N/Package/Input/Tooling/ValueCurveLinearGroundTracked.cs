using UnityEngine;

namespace N.Package.Input.Tooling
{
    /// <summary>
    /// Allow linear velocity only while grounded.
    /// </summary>
    [System.Serializable] 
    public class ValueCurveLinearGroundTracked : ValueCurve
    {
        public InputGroundTracker groundTracker;
        
        public float acceleration;
        
        public float dampening;

        public float energyPool;

        public float energy;

        [Tooltip("Use energy this fast when active")]
        public float energyUseRate;

        [Tooltip("Gain energy this fast when grounded")]
        public float energyGainRate;
        
        protected override float Next(float step)
        {
            if (groundTracker == null) return value;
            
            // Use energy
            if (!groundTracker.state.grounded)
            {
                energy = Mathf.Clamp(energy - Time.deltaTime * energyUseRate, 0f, energyPool);
            }

            // Gain energy
            else
            {
                energy = Mathf.Clamp(energy + Time.deltaTime * energyGainRate, 0f, energyPool);
            }
            
            // Update value
            var delta = acceleration * step * Time.deltaTime;
            if (Mathf.Abs(delta) > 0)
            {
                if (energy > 0)
                {
                    return value + delta;
                }
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