using N;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace N.Package.Input.Next.Draggable
{
    /// Implement this interface by things that can be dragged by clicking on them.
    public interface IDraggableSource
    {
        /// Return the factory to create a cursor instance, if one is required
        GameObject CursorFactory { get; }

        /// Access directly to the current cursor object for this object, if any.
        GameObject DragCursor { get; set; }

        /// Return the primary game object for this
        GameObject GameObject { get; }

        /// Is this still a valid draggable?
        /// Sometimes dragging objects into specific places automatically 'drops' them.
        /// This is set to true if CanDragStart return true; if it set false later,
        /// the drag ends.
        bool IsValid { get; set; }

        /// Return if the target should be dragged along with the cursor
        bool DragObject { get; }

        /// Invoked to check if an object is ready to be dragged
        bool CanDragStart();

        /// Invoked when the dragged object start a drag operation.
        void OnDragStart();

        /// Invoked when the dragged object stops over nothing valid.
        void OnDragStop();

        /// Invoked when the dragged object is released.
        void OnReceivedBy(IDraggableReceiver receiver);

        /// Invoked when the dragged object moves over a target that can accept it.
        /// @param valid IF the receiver is valid for this draggable
        void EnterTarget(IDraggableReceiver target, bool valid);

        /// Invoked when the dragged object moves off a receiver
        void ExitTarget(IDraggableReceiver target);
    }
}
