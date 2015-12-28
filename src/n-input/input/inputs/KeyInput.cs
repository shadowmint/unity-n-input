using N;
using UnityEngine;
using System.Collections.Generic;

namespace N.Package.Input {

  /// A pick event
  public class KeyDownEvent : N.Event {

    /// The key that was pressed
    public KeyCode keycode;
  }

  /// A pick event
  public class KeyUpEvent : N.Event {

    /// The key that was pressed
    public KeyCode keycode;
  }

  /// An input handler for collecting keyboard input
  public class KeyInput : RawInputHandler {

    /// Singleton
    private static KeyInput instance = null;

    /// The set of keycodes to look at
    public List<KeyCode> keycodes = new List<KeyCode>();

    /// Enable this input type
    public static void Enable() {
      if (instance == null) {
        instance = new KeyInput();
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

    /// Add one more keycode to this poll
    public static void Key(KeyCode code) {
      if (instance != null) {
        if (!instance.keycodes.Contains(code)) {
          instance.keycodes.Add(code);
        }
      }
    }

    /// Check for inputs periodically
    public void Update(Events events) {
    }

    /// Check for inputs every frame, regardless
    public void UpdateFrame(Events events) {
      foreach (var code in keycodes) {
        if (UnityEngine.Input.GetKeyDown(code)) {
          events.Trigger(new KeyDownEvent() { keycode = code });
        }
        else if (UnityEngine.Input.GetKeyUp(code)) {
          events.Trigger(new KeyUpEvent() { keycode = code });
        }
      }
    }
  }

  #if UNITY_EDITOR
  public class KeyInputTests {
    public void test_key_input() {
      KeyInput.Enable();
      KeyInput.Key(KeyCode.Space);
      RawInput.Event((ev) => {
        ev.As<KeyDownEvent>().Then((ep) => {
          // ...
        });
      });
    }
  }
  #endif
}
