using N;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace N.Input.Draggable
{
    /// Use this to run event targets on a DraggableReceiver
    public class ReceiveTarget
    {
        /// The source object
        public DraggableSource source;

        /// Set this to true to accept the target.
        public bool accept;
    }

    /// Makes it possible to drag a Draggable from the GameObject
    [AddComponentMenu("N/Input/Draggable/Draggable Receiver")]
    public class DraggableReceiver : MonoBehaviour
    {
        [Serializable]
        public class ReceiverEvent : UnityEvent<ReceiveTarget> { }

        [Serializable]
        public class DraggableEvent : UnityEvent<DraggableSource> { }

        [SerializeField]
        private ReceiverEvent check = new ReceiverEvent();

        [SerializeField]
        private DraggableEvent accept = new DraggableEvent();

        [SerializeField]
        private DraggableEvent leave = new DraggableEvent();

        /// Return true or false and update state depending on if this receiver
        /// can accept the draggable object.
        public ReceiverEvent onCheckDraggable
        {
            get { return check; }
            set { check = value; }
        }

        /// The draggable has been dropped here, deal with it
        /// This can only be invoked if onCheckSourceDraggable accepted
        public DraggableEvent onAcceptDraggable
        {
            get { return accept; }
            set { accept = value; }
        }

        /// Invoked if the draggable leaves this receiver without dropping
        /// anything on it.
        public DraggableEvent onLeaveDraggable
        {
            get { return leave; }
            set { leave = value; }
        }

        /// Default receiver for an over/out event
        public void DefaultReceiverHandler(ReceiveTarget target)
        {
            target.accept = true;
            throw new Exception("Default handler is for example purposes only.");
        }

        /// Default receiver for a drop/click event
        public void DefaultAcceptHandler(DraggableSource target)
        {
            throw new Exception("Default handler is for example purposes only.");
        }

        /// Default leave event
        public void DefaultLeaveHandler(DraggableSource target)
        {
            throw new Exception("Default handler is for example purposes only.");
        }
    }
}
