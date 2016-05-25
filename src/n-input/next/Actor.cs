using UnityEngine;
using System.Collections.Generic;

namespace N.Package.Input.Next
{
    /// An input binding that maps input events into game actions.
    public class Actor : MonoBehaviour
    {
        [Tooltip("The currently assigned controller type, if any")]
        public Controller controller;

        /// The last controller we assigned events to
        private Controller last = null;

        /// Execute an action on this actor
        public virtual void Trigger<TAction>(TAction action)
        {
        }

        /// Process any actions on the actor that are pending
        protected void TriggerPending<TAction>()
        {
            if (controller != null)
            {
                if (controller.actor != this)
                {
                    controller.actor = this;
                }
                if (last != controller)
                {
                    last = controller;
                    controller.OnActorAttached(this);
                }
                foreach (var action in controller.Actions<TAction>())
                {
                    Trigger(action);
                }
            }
        }
    }
}
