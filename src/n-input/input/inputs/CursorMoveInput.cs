using N;
using UnityEngine;
using System.Collections.Generic;
using N.Package.Input.Impl.CursorMoveInput;
using N.Package.Events;

namespace N.Package.Input
{
    /// Input types
    public enum CursorMove {
        MOVE,
        ENTER,
        LEAVE
    }

    /// Base event type
    public class CursorMoveEventBase : IEvent
    {
        /// The GameObject instance
        public GameObject target;

        /// The type, if any, that triggered this event
        public System.Type type;

        /// The coordinates where the raycast intersected the GameObject
        public Vector3 point;
    }

    /// This event is fired constantly while the cursor is over a target
    public class CursorOverEvent : CursorMoveEventBase {}

    /// This event is fired when the cursor moves over the target GameObject
    /// if the object is not currently active.
    public class CursorEnterEvent : CursorMoveEventBase {}

    /// This event is fired when the cursor moves out from the target GameObject,
    /// but Only if the object is currently active.
    public class CursorLeaveEvent : CursorMoveEventBase {}

    /// This input handler tracks raycasts from the cursor constantly and
    /// triggers events for it when-ever it is over it's backing target.
    /// Notice that unlike the CursorPickInput, this input will constantly
    /// spam events.
    public class CursorMoveInput : RawInputHandler
    {
        /// Singleton
        private static CursorMoveInput instance = null;

        /// Active targets
        private ActiveTargetGroup active = new ActiveTargetGroup();

        /// The layerMask
        public int layerMask;

        /// The raycast distance
        public float raycastDistance;

        /// The component filter, or null
        public List<System.Type> componentFilters = new List<System.Type>();

        /// The instance to tracks hits on, or null
        public List<GameObject> targets = new List<GameObject>();

        /// Enable this input type
        public static void Enable(float distance = 100f, int layerMask = Physics.DefaultRaycastLayers)
        {
            if (instance != null)
            {
                _.Log("Warning: Used static CursorMoveInput.Enable() multiple times.");
                _.Log("Consider creating your own instance if you need multiple instances.");
            }
            else
            {
                instance = new CursorMoveInput()
                {
                    raycastDistance = distance,
                    layerMask = layerMask,
                };
                RawInput.Register(instance);
            }
        }

        /// Add a component type to track
        public static void Track(System.Type componentType)
        {
            if (instance != null)
            {
                if (!instance.componentFilters.Contains(componentType))
                {
                    instance.componentFilters.Add(componentType);
                }
            }
        }

        /// Add a component instance to track
        public static void Track(GameObject target)
        {
            if (instance != null)
            {
                if (!instance.targets.Contains(target))
                {
                    instance.targets.Add(target);
                }
            }
        }

        /// Disable
        public static void Disable()
        {
            if (instance != null)
            {
                RawInput.Remove(instance);
                instance = null;
            }
        }

        /// See if we can pick objects
        public void Update(Events events)
        {
            // Reset state
            active.Reset();

            // Process hits
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            foreach (var hit in Physics.RaycastAll(ray, raycastDistance, layerMask))
            {
                // Component types
                foreach (var componentType in componentFilters)
                {
                    var cp = hit.collider.transform.GetComponent(componentType);
                    if (cp != null)
                    {
                        events.Trigger(new CursorOverEvent()
                        {
                            target = hit.collider.transform.gameObject,
                            type = componentType,
                            point = hit.point
                        });
                        if (active.Active(hit.collider.transform.gameObject))
                        {
                            events.Trigger(new CursorEnterEvent()
                            {
                                target = hit.collider.transform.gameObject,
                                type = componentType,
                                point = hit.point
                            });
                        }
                    }
                }

                // Instances
                foreach (var instance in targets)
                {
                    if (hit.collider.transform.gameObject == instance)
                    {
                        events.Trigger(new CursorOverEvent()
                        {
                            target = hit.collider.transform.gameObject,
                            point = hit.point
                        });
                        if (active.Active(hit.collider.transform.gameObject))
                        {
                            events.Trigger(new CursorEnterEvent()
                            {
                                target = hit.collider.transform.gameObject,
                                point = hit.point
                            });
                        }
                    }
                }
            }

            // Now trigger leave events for any old targets
            foreach (var old in active.Inactive())
            {
                events.Trigger(new CursorLeaveEvent()
                {
                    target = old
                });
            }

            // Cleanup component list
            active.FilterInactive();
        }

        /// Every frame
        public void UpdateFrame(Events events)
        {
        }
    }
}
