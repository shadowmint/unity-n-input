using UnityEngine;
using N;
using System.Collections.Generic;

namespace N.Package.Input
{
    /// Static api for RawInputHandlers
    public class RawInput
    {
        /// Singleton
        private static RawInputHandlers instance;
        private static RawInputHandlers Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RawInputHandlers();
                }
                return instance;
            }
        }

        /// Add an event listener
        public static void Register(RawInputHandler handler)
        {
            Instance.Register(handler);
        }

        /// Remove an event listener
        public static void Remove(RawInputHandler handler)
        {
            Instance.Remove(handler);
        }

        /// Clear all event handlers and listeners
        public static void Clear()
        {
            Instance.Clear();
        }

        /// Poll all event listeners for input
        public static void Update()
        {
            Instance.Update();
        }

        /// Poll all event listeners for input every frame
        public static void UpdateFrame()
        {
            Instance.UpdateFrame();
        }

        /// Attach an event handler
        public static void Event(N.EventHandler handler)
        {
            Instance.Event(handler);
        }

        /// Trigger an event from an external event source
        public static void Trigger(N.Event item)
        {
            Instance.Trigger(item);
        }
    }

    /// The low level wrapper around Input to handle various input events
    public class RawInputHandlers
    {

        /// The set of event listeners
        private List<RawInputHandler> handlers = new List<RawInputHandler>();

        /// The event handler core
        private Events events = new Events();

        /// Add an event listener
        public void Register(RawInputHandler handler)
        {
            if (!handlers.Contains(handler))
            {
                handlers.Add(handler);
            }
        }

        /// Remove an event listener
        public void Remove(RawInputHandler handler)
        {
            if (handlers.Contains(handler))
            {
                handlers.Remove(handler);
            }
        }

        /// Clear all
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

        /// Add an event handler
        public void Event(N.EventHandler handler)
        {
            events += handler;
        }

        /// Trigger an event
        public void Trigger(N.Event item)
        {
            if (item != null)
            {
                events.Trigger(item);
            }
        }
    }
}
