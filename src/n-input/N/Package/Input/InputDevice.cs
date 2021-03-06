using System;
using N.Package.Input.Infrastructure;
using UnityEngine;

namespace N.Package.Input
{
    public abstract class InputDevice : MonoBehaviour
    {
        /// <summary>
        /// Invoked when this device is associated with a controller.
        /// </summary>
        public abstract void IsConnected(bool connected);

        /// <summary>
        /// Return a typed state object or null.
        /// </summary>
        public abstract TState GetState<TState>() where TState : class;

        /// <summary>
        /// The actor
        /// </summary>
        public InputInternalActorType Actor { get; set; }
    }
}