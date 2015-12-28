using UnityEngine;
using N;

namespace N.Package.Input {

  /// Polls unity input and dispatches events
  [AddComponentMenu("N/Input/Raw Input Listener")]
  public class RawInputListener : MonoBehaviour {

    [Tooltip("The interval to poll events on")]
    public float interval = 0.01f;

    /// The timer this input listener uses
    public Timer timer = new Timer();

    /// Time since last update
    private float elapsed = 0f;

    public void Update() {
      elapsed += timer.Step();
      if (elapsed >= interval) {
        elapsed = 0f;
        RawInput.Update();
      }
      RawInput.UpdateFrame();  // Happens every frame regardless
    }
  }
}
