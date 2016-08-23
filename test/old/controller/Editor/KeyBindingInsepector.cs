using UnityEngine;
using UnityEditor;
using System;
using N;
using N.Package.Controller.Input;

namespace N.Package.Controller {

  [CustomEditor(typeof(KeyBinding))]
  public class KeyBindingInspector : InputHandlerInspector {
  }
}
