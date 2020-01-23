using UnityEngine;

namespace N.Package.Input.Infrastructure
{
    public class InputInternalActorType : MonoBehaviour
    {
        /// <summary>
        /// The currently bound controller, or none.
        /// </summary>
        protected InputController Controller { get; set; }

        /// <summary>
        /// Should we process input this frame?
        /// </summary>
        protected bool ShouldProcessInput { get; set; }

        /// <summary>
        /// Invoked when the active controller changes.
        /// </summary>
        public void OnControllerChange(InputController controller)
        {
            Controller = controller;
        }

        /// <summary>
        /// Invoked to set the actor as actively receiving input or not. 
        /// </summary>
        public void OnInputReady(bool shouldProcessInput)
        {
            ShouldProcessInput = shouldProcessInput;
        }

        /// <summary>
        /// Invoked when the device is disconnected.
        /// </summary>
        public void OnControllerDisconnected()
        {
            Controller = null;
            ShouldProcessInput = false;
        }
    }
}