using UnityEngine;
using N;
using N.Package.Events;
using System.Collections.Generic;

namespace N.Package.Input
{
    /// The low level wrapper around Input to handle various input events
    public class RawInput
    {
        /// Get the default instance
        private static RawInput instance = null;
        public static RawInput Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new RawInput();
                }
                return instance;
            }
        }

        /// The set of event listeners
        private List<IRawInputHandler> handlers = new List<IRawInputHandler>();

        /// The event handler core
        private EventHandler events = new EventHandler();
        public EventHandler Events { get { return events; } }

        /// Add a raw input handler
        public void Register(IRawInputHandler handler)
        {
            if (!handlers.Contains(handler))
            {
                handlers.Add(handler);
            }
        }

        /// Remove a raw input handler
        public void Remove(IRawInputHandler handler)
        {
            if (handlers.Contains(handler))
            {
                handlers.Remove(handler);
            }
        }

        /// Clear everything
        public void Clear()
        {
            events.Clear();
            handlers.Clear();
        }

        /// Poll all event listeners for input
        public void Update()
        {
            handlers.ForEach((h) => h.Update(events));
        }

        /// Poll all event listeners for input every frame
        /// Normally we don't use this, but some input types, like keyboard,
        /// only register on the frame the event occurs in; hence, regardless
        /// of the normal update interval, they need to be updated every frame.
        public void UpdateFrame()
        {
            handlers.ForEach((h) => h.UpdateFrame(events));
        }
    }
}
