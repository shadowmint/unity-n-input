using N.Package.Input.Infrastructure;
using UnityEngine;

namespace N.Package.Input
{
    public class InputController
    {
        public TState GetState<TState>() where TState : class
        {
            if (Device == null) return null;
            return Device.GetState<TState>();
        }

        /// <summary>
        /// The active device.
        /// </summary>
        private InputDevice Device { get; set; }

        /// <summary>
        /// The active actor.
        /// </summary>
        private InputInternalActorType Actor { get; set; }

        public void Connect(InputDevice device)
        {
            if (Device == device) return;
            if (device == null)
            {
                DisconnectDevice();
            }

            Device = device;
            Device.IsConnected(true);

            if (Actor != null)
            {
                Actor.OnInputReady(true);
                Device.Actor = Actor;
            }
        }

        public void Connect(InputInternalActorType actor)
        {
            if (Actor == actor) return;
            if (actor == null)
            {
                DisconnectActor();
                return;
            }

            Actor = actor;
            Actor.OnControllerChange(this);

            if (Device == null) return;
            Actor.OnInputReady(true);
        }

        private void DisconnectActor()
        {
            if (Actor == null) return;
            Actor.OnControllerDisconnected();
            Actor = null;
            
            if (Device != null)
            {
                Device.Actor = null;
            }
        }

        private void DisconnectDevice()
        {
            if (Device == null) return;
            Device.IsConnected(false);
            Device = null;
            Actor.OnInputReady(false);
        }
    }
}