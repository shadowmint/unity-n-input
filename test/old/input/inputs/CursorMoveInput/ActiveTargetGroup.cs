using N;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace N.Package.Input.Impl.CursorMoveInput
{
    /// An active target
    public class ActiveTarget
    {
        /// The GameObject which was active
        public GameObject instance;

        /// If this GameObject was active this update
        public bool active;
    }

    /// A helper to track active targets
    public class ActiveTargetGroup
    {
        /// The set of currently active targets
        public List<ActiveTarget> active = new List<ActiveTarget>();

        /// Reset all active targets for this frame
        public void Reset()
        {
            for (int i = 0; i < active.Count; i++)
            {
                var t = active[i];
                t.active = false;
            }
        }

        /// Add a new GameObject, or mark it as currently active
        public bool Active(GameObject target)
        {
            var found = false;
            for (int i = 0; i < active.Count; i++)
            {
                var t = active[i];
                if (t.instance == target)
                {
                    found = true;
                    t.active = true;
                    break;
                }
            }
            if (!found)
            {
                active.Add(new ActiveTarget { active = true, instance = target });
            }
            return !found;
        }

        /// Yield all inactive nodes
        public IEnumerable<GameObject> Inactive() {
            foreach (var t in active)
            {
                if (!t.active)
                {
                    yield return t.instance;
                }
            }
        }

        /// Filter out any inactive nodes
        public void FilterInactive()
        {
            active = active.Where((x) => x.active).ToList();
        }
    }
}
