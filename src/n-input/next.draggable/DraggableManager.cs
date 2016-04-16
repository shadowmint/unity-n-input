using N;
using N.Package.Input;
using UnityEngine;
using System.Collections.Generic;
using N.Package.Input.Next.Draggable.Internal;
using N.Package.Input.Next;

namespace N.Package.Input.Next.Draggable
{
    /// Handle draggables on the current scene
    /// Only works if a draggable device is configured in Inputs.
    [AddComponentMenu("N/Input/Next/Draggable/Draggable Manager")]
    public class DraggableManager : MonoBehaviour
    {
        [Tooltip("Draggable background reference object")]
        public GameObject referenceBacking = null;

        [Tooltip("Offset from background reference to drag the object in")]
        public Vector3 referenceBackingOffset;

        [Tooltip("Offset from background reference to drag the cursor in")]
        public Vector3 referenceBackingCursorOffset;

        [Tooltip("Use this button for the dragging")]
        public KeyCode button = KeyCode.Mouse0;

        /// Assign the inputs handler to use here, if required
        public Inputs inputs = null;

        /// The cursor input handler
        private CursorInputHandler inputHandler;

        /// Currently down?
        private bool down;

        public void Start()
        {
            // Check state
            if (referenceBacking == null)
            {
                throw new System.Exception("Draggable manager must have a backing target set");
            }
            referenceBacking.SetActive(true);

            // Setup handler
            inputHandler = new CursorInputHandler(referenceBacking);
            inputHandler.AcceptCursor(CodeForKeyCode(button));
            inputHandler.objectOffset = referenceBackingOffset;
            inputHandler.cursorOffset = referenceBackingCursorOffset;
        }

        // Process inputs
        public void Update()
        {
            // Track button clicks
            foreach (var buttons in Inputs.Default.Stream<Buttons>())
            {
                if (buttons.down(button) && !down)
                {
                    foreach (var hit in CurrentDraggables())
                    {
                        down = true;
                        inputHandler.CursorDown(CodeForKeyCode(button), hit);
                    }
                }
                else if (buttons.up(button) && down)
                {
                    inputHandler.CursorUp(CodeForKeyCode(button));
                    down = false;
                }
            }

            // Entered a new target?
            // TODO: Handle these somehow?
            /*
            RawInput.Default.Events.AddEventHandler<CursorEnterEvent>((evp) =>
            { inputHandler.CursorEnter(evp.target); });

            RawInput.Default.Events.AddEventHandler<CursorLeaveEvent>((evp) =>
            { inputHandler.CursorLeave(evp.target); });
            */

            // Motion~
            var motion = CurrentMotion();
            if (motion.target != null)
            {
                inputHandler.CursorMove(motion.target, motion.point);
            }
        }

        /// Return the code for a key code
        private int CodeForKeyCode(KeyCode code)
        {
            switch (code)
            {
                case KeyCode.Mouse0:
                    return 0;
                case KeyCode.Mouse1:
                    return 1;
            }
            return -1;
        }

        /// Yield the set of intersections on the drag plane
        private Hit CurrentMotion()
        {
            var rtn = new Hit() { target = null };
            foreach (var input in Inputs.Default.Stream<Collider3>())
            {
                foreach (var hit in input.Hits())
                {
                    if (hit.target == referenceBacking)
                    {
                        rtn = hit;
                        break;
                    }
                }
            }
            return rtn;
        }

        /// Yield the set of draggable targets
        private IEnumerable<GameObject> CurrentDraggables()
        {
            foreach (var input in Inputs.Default.Stream<Collider3>())
            {
                foreach (var hit in input.Hits())
                {
                    var draggable = hit.target.GetComponent<DraggableBase>();
                    if (draggable != null)
                    {
                        yield return hit.target;
                    }
                }
            }
        }

        /// Return the Input for this object
        private Inputs CurrentInput
        {
            get
            {
                return inputs ?? Inputs.Default;
            }
        }
    }
}
