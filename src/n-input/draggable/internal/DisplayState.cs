using N;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace N.Package.Input.Draggable.Internal
{
    /// Look after the display while dragging things
    public class DisplayState
    {
        /// Currently active draggable
        public IDraggableSource source;

        /// The cursor object, if any
        public GameObject cursor;

        /// Create a new instance
        public DisplayState(IDraggableSource source)
        {
            this.source = source;
        }

        /// Start dragging, create cursor is required
        public void StartDragging()
        {
        }

        /// Stop dragging, destroy cursor if required
        public void StopDragging()
        {
        }

        /// Process move events
        public void Move(Vector3 position)
        {
        }
    }
}
