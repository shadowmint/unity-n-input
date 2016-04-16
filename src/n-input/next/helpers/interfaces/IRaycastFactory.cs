using UnityEngine;
using System.Collections.Generic;

namespace N.Package.Input.Next.Helpers
{

    /// API for world position
    public interface IRaycastFactory
    {
        /// Return the ray to cast along
        Ray Ray { get; }

        /// Return the distance
        float Distance { get; }

        /// Return the cast filter
        int LayerMask { get; }

        /// Set and get the total number of hits
        int Count { get; set; }
    }

    /// Helper functions
    public static class RaycastFactoryHelpers
    {
        /// Raycast out from a point and collect all intersecting objects
        public static IEnumerable<Hit> Raycast(this IRaycastFactory self)
        {
            Hit point;
            var hits = Physics.RaycastAll(self.Ray, self.Distance, self.LayerMask);
            self.Count = hits.Length;
            foreach (var hit in hits)
            {
                point.point = hit.point;
                point.target = hit.rigidbody.gameObject;
                point.distance = hit.distance;
                point.normal = hit.normal;
                yield return point;
            }
        }

        /// Raycast out from a point and collect all intersecting objects
        public static IEnumerable<Hit> Raycast2D(this IRaycastFactory self)
        {
            Hit point;
            var hits = Physics2D.RaycastAll(self.Ray.origin, Vector2.zero, self.LayerMask);
            self.Count = hits.Length;
            foreach (var hit in hits)
            {
                if (hit.collider != null) {
                  point.point = hit.point;
                  point.target = hit.collider.gameObject;
                  point.distance = hit.distance;
                  point.normal = hit.normal;
                  yield return point;
                }
            }
        }
    }
}
