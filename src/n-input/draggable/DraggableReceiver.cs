using N;
using UnityEngine;
using UnityEngine.Events;
using System;
using N.Package.Input.Draggable.Internal;

namespace N.Package.Input.Draggable
{
    /// Makes it possible to drag a Draggable from the GameObject
    [AddComponentMenu("N/Input/Draggable/Draggable Receiver")]
    public class DraggableReceiver : DraggableBase, IDraggableReceiver
    {
        [Tooltip("Check if a draggable is valid for this receiver")]
        public CheckSourceEvent isValid = new CheckSourceEvent();

        [Tooltip("Handle a draggalbe which is dropped on this receiver")]
        public DraggableEvent onAccept = new DraggableEvent();

        [Tooltip("Invoked if the draggable leaves this receiver.")]
        public DraggableEvent onLeave = new DraggableEvent();

        [Tooltip("Invoked if the draggable enters this receiver.")]
        public EnterEvent onEnter = new EnterEvent();

        /// Makes sure an instance exists on start
        public void Start()
        {
            Draggable.RequireManager();
        }

        /// Receiver api
        public override IDraggableReceiver Receiver { get { return this; } }

        public GameObject GameObject { get { return this.gameObject; } }

        public bool IsValidDraggable(IDraggableSource draggable)
        {
            if (isValid.GetPersistentEventCount() > 0)
            {
                var check = new DraggableIsValid();
                check.source = draggable;
                check.accept = false;
                isValid.Invoke(check);
                return check.accept;
            }
            return false;
        }

        public void DraggableEntered(IDraggableSource draggable, bool isValid)
        {
            if (onEnter.GetPersistentEventCount() > 0)
            {
                var enter = new DraggableEntered();
                enter.source = draggable;
                enter.valid = isValid;
                onEnter.Invoke(enter);
            }
        }

        public void DraggableLeft(IDraggableSource draggable)
        {
            if (onLeave.GetPersistentEventCount() > 0)
            {
                onLeave.Invoke(draggable);
            }
        }

        public void OnReceiveDraggable(IDraggableSource draggable)
        {
            if (onAccept.GetPersistentEventCount() > 0)
            {
                onAccept.Invoke(draggable);
            }
        }
    }
}
