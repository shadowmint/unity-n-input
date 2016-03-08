using N;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace N.Package.Input.Draggable.Internal
{
    public class CursorInputHandler
    {
        /// This object is the object that we drag over
        private GameObject dragPlane;

        /// Add a valid cursor id to track
        private List<int> cursors = new List<int>();

        /// Pool of active objects
        private ActivePool pool = new ActivePool();

        /// Create a new instance, providing a drag plane
        public CursorInputHandler(GameObject dragPlane)
        {
            this.dragPlane = dragPlane;
        }

        /// Add a cursor id that is valid to track
        public void AcceptCursor(int id)
        {
            if (!cursors.Contains(id))
            {
                cursors.Add(id);
            }
        }

        /// Check if a cursor is a valid id
        public bool IsValidCursor(int id)
        {
            return cursors.Contains(id);
        }

        /// Return true if any drag is currently happening
        public bool Busy()
        {
            return pool.Count > 0;
        }

        /// Handle a cursor pick
        public void CursorDown(int cursorId, GameObject target)
        {
            foreach (var draggable in target.GetComponentsInChildren<DraggableBase>())
            {
                if (draggable.Source != null)
                {
                    if (draggable.Source.CanDragStart())
                    {
                        draggable.Source.OnDragStart();
                        pool.StartDragging(draggable.Source);
                    }
                }
            }
        }

        public void CursorUp(int cursorId)
        {
            pool.StopDragging();
        }

        public void CursorEnter(GameObject target)
        {
            foreach (var draggable in target.GetComponentsInChildren<DraggableBase>())
            {
                if (draggable.Receiver != null)
                {
                    pool.ProcessReceiver(draggable.Receiver, true);
                }
            }
        }

        public void CursorLeave(GameObject target)
        {
            foreach (var draggable in target.GetComponentsInChildren<DraggableBase>())
            {
                if (draggable.Receiver != null)
                {
                    pool.ProcessReceiver(draggable.Receiver, false);
                }
            }
        }

        public void CursorMove(GameObject target, Vector3 intersectsAt)
        {
            if (target == dragPlane)
            {
                pool.Move(intersectsAt);
            }
        }
    }
}
