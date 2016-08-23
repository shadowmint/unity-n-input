using N;
using UnityEngine;
using System.Collections.Generic;
using N.Package.Events;

namespace N.Package.Input {

  /// A pick event
  public class KeyDownEvent : IEvent
  {
      /// Set and get access to the event helper api
      public IEventApi Api { get; set; }

      /// The key that was pressed
      public KeyCode keycode;
  }

  /// A pick event
  public class KeyUpEvent : IEvent
  {
      /// Set and get access to the event helper api
      public IEventApi Api { get; set; }

      /// The key that was pressed
      public KeyCode keycode;
  }

  /// An input handler for collecting keyboard input
  public class KeyInput : IRawInputHandler {

    /// Singleton
    private static KeyInput instance = null;

    /// The set of keycodes to look at
    public List<KeyCode> keycodes = new List<KeyCode>();

    /// Enable this input type
    public static void Enable() {
      if (instance == null) {
        instance = new KeyInput();
        RawInput.Default.Register(instance);
      }
    }

    /// Disable
    public static void Disable() {
      if (instance != null) {
        RawInput.Default.Remove(instance);
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
    public void Update(EventHandler events) {
    }

    /// Check for inputs every frame, regardless
    public void UpdateFrame(EventHandler events) {
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
}
