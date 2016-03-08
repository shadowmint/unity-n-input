using N;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace N.Package.Input.Draggable.Internal
{
    public class ActivePool
    {
        /// Set of active targets
        private IDictionary<IDraggableSource, ActiveObject> pool = new Dictionary<IDraggableSource, ActiveObject>();

        /// Return number of draggables
        public int Count
        {
            get
            {
                return pool.Count;
            }
        }

        /// Start a new draggable
        public void StartDragging(IDraggableSource draggable)
        {
            if (!pool.ContainsKey(draggable))
            {
                var active = new ActiveObject(draggable);
                pool.Add(draggable, active);
                active.display.StartDragging();
            }
        }

        /// Release all draggables
        public void StopDragging()
        {
            foreach (var op in pool.Values)
            {
                var count = op.ProcessReceivers();
                if (count == 0)
                {
                    op.source.OnReceivedBy(null);  // Released with no receiver
                }
                op.display.StopDragging();
            }
            pool.Clear();
        }

        /// Add a receiver to a draggable
        public void AddReceiver(IDraggableSource draggable, IDraggableReceiver receiver, bool valid)
        {
            if (pool.ContainsKey(draggable))
            {
                // Cannot drop self onto self.
                if (draggable.GameObject != receiver.GameObject)
                {
                    var active = pool[draggable];
                    if (valid)
                    {
                        if (!active.receivers.Contains(receiver))
                        {
                            active.source.OverValidTarget(receiver);
                            receiver.DraggableEntered(draggable, valid);
                            active.receivers.Add(receiver);
                        }
                    }
                    else
                    {
                        if (!active.invalid.Contains(receiver))
                        {
                            active.source.OverInvalidTarget(receiver);
                            receiver.DraggableEntered(draggable, valid);
                            active.invalid.Add(receiver);
                        }
                    }
                }
            }
        }

        /// Remove a receiver from a draggable
        public void RemoveReceiver(IDraggableSource draggable, IDraggableReceiver receiver)
        {
            if (pool.ContainsKey(draggable))
            {
                var active = pool[draggable];
                if (active.receivers.Contains(receiver))
                {
                    receiver.DraggableLeft(draggable);
                    active.receivers.Remove(receiver);
                }
                else if (active.invalid.Contains(receiver))
                {
                    receiver.DraggableLeft(draggable);
                    active.invalid.Remove(receiver);
                }
            }
        }

        /// Process a new incoming receiver and dispatch it to any draggable as required
        public void ProcessReceiver(IDraggableReceiver receiver, bool add)
        {
            foreach (var active in pool.Values)
            {
                if (add)
                {
                    var accept = receiver.IsValidDraggable(active.source);
                    AddReceiver(active.source, receiver, accept);
                }
                else
                {
                    RemoveReceiver(active.source, receiver);
                }
            }
        }

        /// Process move event
        public void Move(Vector3 intersectAt)
        {
            foreach (var op in pool.Values)
            {
                op.display.Move(intersectAt);
            }
        }
    }
}
