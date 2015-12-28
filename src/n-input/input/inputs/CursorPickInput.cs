using N;
using UnityEngine;

namespace N.Package.Input {

  /// A pick event
  public class CursorPickEvent : N.Event {

    /// The id of the pointer that triggered this event
    public int pointerId;

    /// GameObject
    public GameObject hit;
  }

  /// An input handler for picking objects on the scene.
  public class CursorPickInput : RawInputHandler {

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
    public static void Enable(float distance = 100f, int layerMask = Physics.DefaultRaycastLayers, System.Type componentFilter = null) {
      if (instance == null) {
        instance = new CursorPickInput() {
          raycastDistance = distance,
          layerMask = layerMask,
          componentFilter = componentFilter
        };
        RawInput.Register(instance);
      }
    }

    /// Disable
    public static void Disable() {
      if (instance != null) {
        RawInput.Remove(instance);
        instance = null;
      }
    }

    /// See if we can pick objects
    public void Update(Events events) {
      var id = PointerId();
      if (id != -1) {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
        foreach (var hit in Physics.RaycastAll(ray, raycastDistance, layerMask)) {
          if (componentFilter != null) {
            var cp = hit.collider.transform.GetComponent(componentFilter);
            if (cp != null) {
              events.Trigger(new CursorPickEvent() {
                pointerId = id,
                hit = hit.collider.transform.gameObject
              });
            }
          }
          else {
            events.Trigger(new CursorPickEvent() {
              pointerId = id,
              hit = hit.collider.transform.gameObject
            });
          }
        }
      }
    }

    /// Every frame
    public void UpdateFrame(Events events) {
    }

    /// Get the id of the currently pressed pointer, or -1
    private int PointerId() {
      if (UnityEngine.Input.GetMouseButtonDown(0)) {
        return 0;
      }
      else if (UnityEngine.Input.GetMouseButtonDown(1)) {
        return 1;
      }
      else if (UnityEngine.Input.GetMouseButtonDown(2)) {
        return 2;
      }
      return -1;
    }
  }

  #if UNITY_EDITOR
  public class CursorPickTests {
    public void test_pick_target() {
      CursorPickInput.Enable();
      RawInput.Event((ev) => {
        ev.As<CursorPickEvent>().Then((ep) => {
          // ...
        });
      });
    }
  }
  #endif
}
