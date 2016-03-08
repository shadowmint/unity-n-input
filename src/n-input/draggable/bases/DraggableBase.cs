using N;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace N.Package.Input.Draggable
{
    /// Return the source or receiver for this component
    public abstract class DraggableBase : MonoBehaviour
    {
        /// The source if this component provides one
        public IDraggableSource Source { get { return null; } }

        /// The receiver if this componen
        public IDraggableReceiver Receiver { get { return null; } }
    }
}
