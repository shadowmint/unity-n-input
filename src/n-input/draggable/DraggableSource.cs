using N;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace N.Package.Input.Draggable
{
    /// Makes it possible to drag a Draggable from the GameObject
    [AddComponentMenu("N/Input/Draggable/Draggable Source")]
    public class DraggableSource : MonoBehaviour
    {
        [Tooltip("When dragging, create an instance of this cursor icon on the cursor")]
        public GameObject cursor;

        [Tooltip("When dragging, drag the object itself as well as showing the cursor")]
        public bool dragSelf;

        [Tooltip("The origin for where this drag started")]
        public Vector3 origin;

        [Serializable]
        public class DraggableEvent : UnityEvent<DraggableSource> { }

        [SerializeField]
        private DraggableEvent accept = new DraggableEvent();

        [SerializeField]
        private DraggableEvent reject = new DraggableEvent();

        [SerializeField]
        private DraggableEvent idle = new DraggableEvent();

        [SerializeField]
        private DraggableEvent dropped = new DraggableEvent();

        /// Invoked when the draggable is accepted by a target
        public DraggableEvent onDraggableAccepted
        {
            get { return accept; }
            set { accept = value; }
        }

        /// Invoked when the draggable is rejected by a target
        public DraggableEvent onDraggableRejected
        {
            get { return reject; }
            set { reject = value; }
        }

        /// Invoked when the draggable becomes idle
        public DraggableEvent onDraggableIdle
        {
            get { return idle; }
            set { idle = value; }
        }

        /// Invoked when the draggable is dropped in the middle of no where
        /// Notice this is not invoked if the target accepts the draggable
        public DraggableEvent onDraggableDropped
        {
            get { return dropped; }
            set { dropped = value; }
        }

        /// Current active cursor object
        private GameObject cursorInstance;

        /// Generate and return a new cursor for this object
        public GameObject GetCursor() {
            GameObject rtn = cursorInstance;
            if ((rtn == null) && (cursor != null))
            {
                Scene.Spawn(cursor).Then((cp) => { rtn = cp; });
            }
            cursorInstance = rtn;
            return rtn;
        }

        /// Destroy the cursor for this object
        public void DestroyCursor()
        {
            if (cursorInstance != null)
            {
                GameObject.Destroy(cursorInstance);
            }
        }

        /// Makes sure a manager exists
        public void Start()
        {
            Draggable.RequireManager();
        }
    }
}
