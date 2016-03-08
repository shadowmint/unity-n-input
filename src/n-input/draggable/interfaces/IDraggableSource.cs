using N;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace N.Package.Input.Draggable
{
    /// Implement this interface by things that can be dragged by clicking on them.
    public interface IDraggableSource
    {
        /// Return the cursor to create when dragging, if one is required.
        GameObject DragCursor { get; }

        /// Return the primary game object for this
        GameObject GameObject { get; }

        /// Return if the target should be dragged along with the cursor
        bool DragObject { get; }

        /// Invoked to check if an object is ready to be dragged
        bool CanDragStart();

        /// Invoked when the dragged object start a drag operation, but is not
        /// over any valid or invalid targets.
        void OnDragStart();

        /// Invoked when the dragged object is released.
        /// @param target If over a valid target, the target is supplied.
        void OnReceivedBy(IDraggableReceiver receiver);

        /// Invoked when the dragged object moves over a target that can accept it.
        void OverValidTarget(IDraggableReceiver target);

        /// Invoked when the dragged object moves over a target that will not accept it.
        /// This is for explicit rejection, eg. trying to click on a disabled button.
        void OverInvalidTarget(IDraggableReceiver target);
    }
}
