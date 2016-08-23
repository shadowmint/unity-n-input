using N;
using UnityEngine;
using N.Package.Events;

namespace N.Package.Input
{
    /// A pick event
    public class CursorPickEvent : IEvent
    {
        /// Set and get access to the event helper api
        public IEventApi Api { get; set; }

        /// The id of the pointer that triggered this event
        public int pointerId;

        /// GameObject
        public GameObject hit;

        /// Is this a start or stop?
        public bool active;
    }

    /// An input handler for picking objects on the scene.
    public class CursorPickInput : IRawInputHandler
    {
        /// Singleton
        private static CursorPickInput instance = null;

        /// The layerMask
        public int layerMask;

        /// The raycast distance
        public float raycastDistance;

        /// The component filter
        /// Only return hits on objects with this component type.
        public System.Type componentFilter;

        /// Enable this input type
        public static void Enable(float distance = 100f, int layerMask = Physics.DefaultRaycastLayers, System.Type componentFilter = null)
        {
            if (instance == null)
            {
                instance = new CursorPickInput()
                {
                    raycastDistance = distance,
                    layerMask = layerMask,
                    componentFilter = componentFilter
                };
                RawInput.Default.Register(instance);
            }
        }

        /// Disable
        public static void Disable()
        {
            if (instance != null)
            {
                RawInput.Default.Remove(instance);
                instance = null;
            }
        }

        /// See if we can pick objects
        public void Update(EventHandler events)
        {
            bool matched = false;
            bool active;
            var id = PointerId(out active);
            if (id != -1)
            {
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                foreach (var hit in Physics.RaycastAll(ray, raycastDistance, layerMask))
                {
                    if (componentFilter != null)
                    {
                        var cp = hit.collider.transform.GetComponent(componentFilter);
                        if (cp != null)
                        {
                            matched = true;
                            events.Trigger(new CursorPickEvent()
                            {
                                active = active,
                                pointerId = id,
                                hit = hit.collider.transform.gameObject
                            });
                        }
                    }
                    else
                    {
                        matched = true;
                        events.Trigger(new CursorPickEvent()
                        {
                            active = active,
                            pointerId = id,
                            hit = hit.collider.transform.gameObject
                        });
                    }
                }

                // Send notification that the cursor, was, generally speaking, released.
                if (!matched)
                {
                    events.Trigger(new CursorPickEvent()
                    {
                        pointerId = id,
                        hit = null,
                        active = false
                    });
                }
            }
        }

        /// Every frame
        public void UpdateFrame(EventHandler events)
        {
        }

        /// Get the id of the currently pressed pointer, or -1
        private int PointerId(out bool active)
        {
            active = false;
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                active = true;
                return 0;
            }
            else if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                active = true;
                return 1;
            }
            else if (UnityEngine.Input.GetMouseButtonDown(2))
            {
                active = true;
                return 2;
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                return 0;
            }
            else if (UnityEngine.Input.GetMouseButtonUp(1))
            {
                return 1;
            }
            else if (UnityEngine.Input.GetMouseButtonUp(2))
            {
                return 2;
            }
            return -1;
        }
    }
}
