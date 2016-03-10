using N;
using N.Package.Input;
using UnityEngine;
using N.Package.Input.Draggable.Internal;

namespace N.Package.Input.Draggable
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

        /// The cursor input handler
        private CursorInputHandler inputHandler;

        public void Start()
        {
            // Check state
            if (referenceBacking == null)
            {
                throw new System.Exception("Draggable manager must have a backing target set");
            }

            // Setup handler
            inputHandler = new CursorInputHandler(referenceBacking);
            inputHandler.AcceptCursor(0);
            inputHandler.objectOffset = referenceBackingOffset;
            inputHandler.cursorOffset = referenceBackingCursorOffset;

            // Bind input events for clicks on targets
            CursorPickInput.Enable(raycastDistance, layerMask, typeof(DraggableBase));
            CursorMoveInput.Enable(raycastDistance, layerMask);
            CursorMoveInput.Track(referenceBacking);
            CursorMoveInput.Track(typeof(DraggableBase));

            // If we get up or down on a cursor, start dragging or stop dragging
            RawInput.Default.Events.AddEventHandler<CursorPickEvent>((evp) =>
            {
                if (evp.active)
                {
                    inputHandler.CursorDown(evp.pointerId, evp.hit);
                }
                else
                {
                    inputHandler.CursorUp(evp.pointerId);
                }
            });

            // Receiver updates
            RawInput.Default.Events.AddEventHandler<CursorEnterEvent>((evp) =>
            { inputHandler.CursorEnter(evp.target); });

            RawInput.Default.Events.AddEventHandler<CursorLeaveEvent>((evp) =>
            { inputHandler.CursorLeave(evp.target); });

            RawInput.Default.Events.AddEventHandler<CursorOverEvent>((evp) =>
            { inputHandler.CursorMove(evp.target, evp.point); });
        }
    }
}
