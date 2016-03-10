using N;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace N.Package.Input.Draggable
{
    /// Check if the source is valid for a receiver
    public class DraggableIsValid
    {
        /// The source object
        public IDraggableSource source;

        /// Set this to true to accept the target.
        public bool accept;
    }

    /// Notify that a draggable has entered
    public class DraggableEntered
    {
        /// The source object
        public IDraggableSource source;

        /// Is the draggable valid?
        public bool valid;
    }

    /// Notify that a receiver has been moved over
    public class DraggableOverReceiver
    {
        /// The source object
        public IDraggableReceiver receiver;

        /// Is the receiver valid?
        public bool valid;
    }

    /// Check if a draggable is valid for drag start
    public class DraggableCanStart
    {
        /// Is the draggable valid?
        public bool accept;
    }

    [Serializable]
    public class CheckReadyEvent : UnityEvent<DraggableCanStart> { }

    [Serializable]
    public class CheckSourceEvent : UnityEvent<DraggableIsValid> { }

    [Serializable]
    public class EnterEvent : UnityEvent<DraggableEntered> { }

    [Serializable]
    public class OverEvent : UnityEvent<DraggableOverReceiver> { }

    [Serializable]
    public class DraggableEvent : UnityEvent<IDraggableSource> { }

    [Serializable]
    public class ReceiverEvent : UnityEvent<IDraggableReceiver> { }
}
