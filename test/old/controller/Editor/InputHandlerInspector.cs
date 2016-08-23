using UnityEngine;
using UnityEditor;
using System;
using N;

namespace N.Package.Controller {

  /// Base class for binding inspectors
  public class InputHandlerInspector : Editor {

    /// Currently selected value
    int index = 0;

    /// The associated controller and game object
    ControllerConfig config;

    /// The last enum we used
    string enumType;

    /// Available options
    string[] options;

    void OnEnable() {
      config = null;
      EnumerateValues();
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
      PollEnumChanged();
      EditorGUILayout.BeginHorizontal();
      var new_index = EditorGUILayout.Popup("Action", index, options, EditorStyles.popup);
      EditorGUILayout.EndHorizontal();
      if (index != new_index) {
        index = new_index;
        var script = (InputHandler) target;
        script.eventId = options[new_index];
      }
    }

    /// Check if the enum has changed, and renumerate if required
    void PollEnumChanged() {
      if (config != null) {
        if (config.qualifiedName != enumType) {
          EnumerateValues();
        }
      }
    }

    /// Find the ControllerBindingControl on this object, and enumerate options.
    void EnumerateValues() {
      var script = (InputHandler) target;
      var scriptObject = script.gameObject;
      config = scriptObject.GetComponent<ControllerConfig>();
      if (config && !string.IsNullOrEmpty(config.qualifiedName)) {
        options = N.Reflect.Enum.Enumerate(config.qualifiedName);
        enumType = config.qualifiedName;
        index = Array.IndexOf(options, script.eventId);
      }
      else {
        options = new string[]{};
      }
    }
  }
}
