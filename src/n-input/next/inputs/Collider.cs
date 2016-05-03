using UnityEngine;
using System.Collections.Generic;
using N.Package.Input.Next.Helpers;

namespace N.Package.Input.Next
{
    /// Simplified uniform collider
    public struct Hit
    {
        /// The target object that the raycast hit
        public GameObject target;

        /// The intersection point
        public Vector3 point;

        /// The intersection normal
        public Vector3 normal;

        /// The distance to the intersection
        public float distance;
    }

    /// API for world position where we intersect with objects
    /// eg. For raycast cursors, or 3d motion controllers
    public class Collider3 : IInput
    {
        protected int id;

        public int Id
        {
            get { return id; }
        }

        /// The factory to raycast with
        public IRaycastFactory Factory { get { return factory; } }
        private IRaycastFactory factory;

        /// Set of collected intersection points
        private Hit[] hits;

        /// Count of current intersections
        private int count;

        /// Last update
        private int lastUpdate = 0;

        public Collider3(int id, IRaycastFactory factory)
        {
            count = 0;
            hits = new Hit[1];
            this.id = id;
            this.factory = factory;
        }

        /// Raycast out from a point and collect all intersecting objects
        public IEnumerable<Hit> Hits()
        {
            Update();
            for (var i = 0; i < count; ++i)
            {
                yield return hits[i];
            }
        }

        /// Update list of hits
        private void Update()
        {
            if (lastUpdate != Time.frameCount)
            {
                count = 0;
                lastUpdate = Time.frameCount;
                var results = Raycast();
                if (results != null)
                {
                    foreach (var hit in results)
                    {
                        if (hits.Length < factory.Count)
                        {
                            hits = new Hit[factory.Count];
                        }
                        hits[count] = hit;
                        count += 1;
                    }
                }
            }
        }

        /// Return an array or null
        private IEnumerable<Hit> Raycast()
        {
            return factory.Raycast();
        }
    }
}
