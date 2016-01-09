using N;
using N.Package.Input;
using UnityEngine;

namespace N.Input.Draggable
{
    /// Handle draggables on the current scene
    [AddComponentMenu("N/Input/Draggable/Draggable Manager")]
    public class DraggableManager : MonoBehaviour
    {
        [Tooltip("Draggable background reference object")]
        public GameObject referenceBacking = null;

        [Tooltip("Offset from background reference to drag the object in")]
        public Vector3 referenceBackingOffset;

        [Tooltip("Offset from background reference to drag the cursor in")]
        public Vector3 referenceBackingCursorOffset;

        [Tooltip("Draggable Source / Receiver layer mask to use")]
        public int layerMask = Physics.DefaultRaycastLayers;

        [Tooltip("Raycast distance to pick targets with")]
        public float raycastDistance = 100f;

        [Tooltip("Is a draggable currently active? If so, this is it.")]
        public GameObject currentDraggable;
        private DraggableSource currentDraggableSource;

        [Tooltip("Is there a currently active receiver? If so, this is it.")]
        public GameObject currentReceiver;
        private DraggableReceiver currentDraggableReceiver;
        private bool acceptedReceiver;

        public void Start()
        {
            // Bind input events for clicks on targets
            CursorPickInput.Enable(raycastDistance, layerMask, typeof(DraggableSource));
            CursorMoveInput.Enable(raycastDistance, layerMask);
            CursorMoveInput.Track(referenceBacking);
            CursorMoveInput.Track(typeof(DraggableReceiver));

            // Check and reset
            currentDraggable = null;
            if (referenceBacking == null)
            {
                throw new System.Exception("Draggable manager must have a backing target set");
            }

            RawInput.Event((ev) =>
            {
                // If we get up or down on a cursor, start dragging or stop dragging
                ev.As<CursorPickEvent>().Then((evp) =>
                {
                    if (evp.active)
                    {
                        StartDragging(evp.hit.GetComponent<DraggableSource>());
                    }
                    else
                    {
                        StopDragging();
                    }
                });

                // Receiver updates
                ev.As<CursorOverEvent>().Then((evp) => { UpdateDrag(evp); });
                ev.As<CursorEnterEvent>().Then((evp) => { EnterReceiver(evp); });
                ev.As<CursorLeaveEvent>().Then((evp) => { LeaveReceiver(evp); });
            });
        }

        /// Enter a Receiver area
        private void EnterReceiver(CursorEnterEvent ev)
        {
            if (currentDraggable != null)
            {
                if (ev.type == typeof(DraggableReceiver))
                {
                    // Save receiver
                    currentDraggableReceiver = ev.target.GetComponent<DraggableReceiver>();
                    currentReceiver = ev.target;

                    // Check if valid?
                    var query = new ReceiveTarget() { source = currentDraggableSource };
                    currentDraggableReceiver.onCheckDraggable.Invoke(query);
                    acceptedReceiver = query.accept;

                    // Update
                    if (acceptedReceiver)
                    {
                        currentDraggableSource.onDraggableAccepted.Invoke(currentDraggableSource);
                    }
                    else
                    {
                        currentDraggableSource.onDraggableRejected.Invoke(currentDraggableSource);
                    }
                }
            }
        }

        /// Exit a Receiver area
        private void LeaveReceiver(CursorLeaveEvent ev)
        {
            if (currentReceiver != null)
            {
                if (ev.target == currentReceiver)
                {
                    currentDraggableSource.onDraggableIdle.Invoke(currentDraggableSource);
                    currentDraggableReceiver.onLeaveDraggable.Invoke(currentDraggableSource);
                    currentReceiver = null;
                    currentDraggableReceiver = null;
                }
            }
            else if (currentDraggable != null)
            {
                currentDraggableSource.onDraggableIdle.Invoke(currentDraggableSource);
            }
        }

        /// Update the current position of the draggable marker
        private void UpdateDrag(CursorOverEvent update)
        {
            if (currentDraggable != null)
            {
                if (update.target == referenceBacking)
                {
                    var target = update.point;
                    var cursor = currentDraggableSource.GetCursor();
                    if (cursor != null)
                    {
                        cursor.Move(target + referenceBackingCursorOffset);
                    }
                    if (currentDraggableSource.dragSelf)
                    {
                        currentDraggable.Move(target + referenceBackingOffset);
                    }
                }
            }
        }

        /// Begin dragging an object
        private void StartDragging(DraggableSource target)
        {
            if ((target != null) && (currentDraggable == null))
            {
                currentDraggable = target.gameObject;
                currentDraggableSource = target;
                currentDraggableSource.origin = target.gameObject.Position();
                currentDraggableSource.onDraggableIdle.Invoke(currentDraggableSource);
            }
        }

        /// Stop dragging an object
        private void StopDragging()
        {
            bool accepted = false;
            if (currentDraggable != null)
            {
                if (currentReceiver != null)
                {
                    if (acceptedReceiver)
                    {
                        currentDraggableReceiver.onAcceptDraggable.Invoke(currentDraggableSource);
                        accepted = true;
                    }
                }
                if (!accepted)
                {
                    currentDraggableSource.onDraggableDropped.Invoke(currentDraggableSource);
                    if (currentReceiver != null)
                    {
                        currentDraggableReceiver.onLeaveDraggable.Invoke(currentDraggableSource);
                    }
                }
                currentDraggableSource.DestroyCursor();
                currentDraggableSource = null;
                currentDraggable = null;
                currentReceiver = null;
                currentDraggableReceiver = null;
            }
        }
    }
}
