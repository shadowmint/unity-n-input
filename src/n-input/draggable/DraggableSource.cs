using N;
using System;
using UnityEngine;
using UnityEngine.Events;
using N.Package.Input.Draggable.Internal;

namespace N.Package.Input.Draggable
{
    /// Makes it possible to drag a Draggable from the GameObject
    [AddComponentMenu("N/Input/Draggable/Draggable Source")]
    public class DraggableSource : DraggableBase, IDraggableSource
    {
        [Tooltip("When dragging, create an instance of this cursor icon on the cursor")]
        public GameObject cursor;

        [Tooltip("When dragging, drag the object itself as well as showing the cursor")]
        public bool dragSelf;

        [Tooltip("The origin for where this drag started")]
        public Vector3 origin;

        [Tooltip("Bind functions to check if drag can start")]
        public CheckReadyEvent canDragStart = new CheckReadyEvent();

        [Tooltip("Bind functions to run when drag starts")]
        public DraggableEvent onStart = new DraggableEvent();

        [Tooltip("Bind functions to run when drag stops")]
        public DraggableEvent onStop = new DraggableEvent();

        [Tooltip("Bind functions to run when drag stops over a valid receiver")]
        public ReceiverEvent onReceived = new ReceiverEvent();

        [Tooltip("Bind functions to run when drag moves over receiver")]
        public OverEvent enterReceiver = new OverEvent();

        [Tooltip("Bind functions to run when drag moves off a receiver")]
        public ReceiverEvent exitReceiver = new ReceiverEvent();

        /// Makes sure a manager exists
        public void Start()
        {
            Draggable.RequireManager();
        }

        /// IDraggableSource
        public override IDraggableSource Source { get { return this; } }

        public GameObject DragCursor { get; set; }

        public GameObject CursorFactory { get { return cursor; } }

        public GameObject GameObject { get { return this.gameObject; } }

        public bool DragObject { get { return dragSelf; } }

        public bool CanDragStart()
        {
            if (canDragStart.GetPersistentEventCount() > 0)
            {
                var args = new DraggableCanStart();
                args.accept = false;
                canDragStart.Invoke(args);
                return args.accept;
            }
            return false;
        }

        public void OnDragStart()
        {
            if (onStart.GetPersistentEventCount() > 0)
            {
                onStart.Invoke(null);
            }
        }

        public void OnDragStop()
        {
            if (onStop.GetPersistentEventCount() > 0)
            {
                onStop.Invoke(null);
            }
        }

        public void OnReceivedBy(IDraggableReceiver receiver)
        {
            if (onReceived.GetPersistentEventCount() > 0)
            {
                onReceived.Invoke(receiver);
            }
        }

        public void EnterTarget(IDraggableReceiver target, bool valid)
        {
            if (enterReceiver.GetPersistentEventCount() > 0)
            {
                var args = new DraggableOverReceiver();
                args.receiver = target;
                args.valid = valid;
                enterReceiver.Invoke(args);
            }
         }

        public void ExitTarget(IDraggableReceiver target)
        {
            if (exitReceiver.GetPersistentEventCount() > 0)
            {
                exitReceiver.Invoke(target);
            }
        }
    }
}
