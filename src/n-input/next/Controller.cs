using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Next
{
    /// An event stream generates TAction instances from some source.
    /// eg. Local input, remote input, AI
    public abstract class Controller : MonoBehaviour
    {
        [Tooltip("Follow actor position on any axis?")]
        public bool followActorPosition = true;

        [Tooltip("Follow actor rotation on any axis?")]
        public bool followActorRotation = false;

        [Tooltip("The actor for this controller")]
        public Actor actor;

        /// Generate the next set of input actions
        public abstract IEnumerable<TAction> Actions<TAction>();

        /// Update position and rotation
        public void Update()
        {
            if (actor != null)
            {
                if (followActorPosition)
                {
                    if (transform.position != actor.transform.position)
                    {
                        transform.position = actor.transform.position;
                    }
                }
                if (followActorRotation)
                {
                    if (transform.rotation != actor.transform.rotation)
                    {
                        transform.rotation = actor.transform.rotation;
                    }
                }
            }
        }
    }
}
