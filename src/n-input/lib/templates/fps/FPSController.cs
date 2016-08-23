using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Templates.FPS
{
  public class FPSController : Controller
  {
    [Tooltip("The FPS Camera we're using, if any")]
    public FPSCamera FPSCamera;

    [Tooltip("Invert look")]
    public bool invertLook = false;

    [Tooltip("Keybindings")]
    public FPSKeyBindings keyBindings = new FPSKeyBindings();

    // Event mapper
    private Binding<FPSAction> binding;

    public void Start()
    {
      // Devices
      var mouse = Devices.Mouse;
      mouse.EnableNormalizedCursor2(UnityEngine.Camera.main);
      Inputs.Default.Register(mouse);
      Inputs.Default.Register(Devices.Keyboard);

      // Inputs: Mouse
      binding = new Binding<FPSAction>(Inputs.Default);
      binding.Bind(new Cursor2Binding<FPSAction>(new Vector2(0f, 0f), new Vector2(1f, 1f), null, null, new FPSLookAtEvent()));

      // Inputs: Keyboard
      binding.Bind(new KeyBinding<FPSAction>(keyBindings.forwards, new FPSMoveEvent(FPSMotion.FORWARDS, true), new FPSMoveEvent(FPSMotion.FORWARDS, false)));
      binding.Bind(new KeyBinding<FPSAction>(keyBindings.backwards, new FPSMoveEvent(FPSMotion.BACKWARDS, true), new FPSMoveEvent(FPSMotion.BACKWARDS, false)));
      binding.Bind(new KeyBinding<FPSAction>(keyBindings.left, new FPSMoveEvent(FPSMotion.LEFT, true), new FPSMoveEvent(FPSMotion.LEFT, false)));
      binding.Bind(new KeyBinding<FPSAction>(keyBindings.right, new FPSMoveEvent(FPSMotion.RIGHT, true), new FPSMoveEvent(FPSMotion.RIGHT, false)));
      binding.Bind(new KeyBinding<FPSAction>(keyBindings.turnLeft, new FPSMoveEvent(FPSMotion.TURN_LEFT, true), new FPSMoveEvent(FPSMotion.TURN_LEFT, false)));
      binding.Bind(new KeyBinding<FPSAction>(keyBindings.turnRight, new FPSMoveEvent(FPSMotion.TURN_RIGHT, true), new FPSMoveEvent(FPSMotion.TURN_RIGHT, false)));
      binding.Bind(new KeyBinding<FPSAction>(keyBindings.jump, new FPSMoveEvent(FPSMotion.JUMP, true), new FPSMoveEvent(FPSMotion.JUMP, false)));
    }

    /// When an actor is bound, bind the FPS camera to it
    public override void OnActorAttached(Actor actor)
    {
      if (FPSCamera != null)
      {
        FPSCamera.target = (actor as FPSActor).head;
      }
    }

    public override IEnumerable<TAction> Actions<TAction>()
    {
      if (typeof(TAction) == typeof(FPSAction))
      {
        foreach (var action in binding.Actions())
        {
          InvertLook(action as FPSLookAtEvent);
          yield return (TAction) (object) action;
        }
      }
    }

    /// Invert action if required
    private void InvertLook(FPSLookAtEvent action)
    {
      if ((action != null) && (invertLook))
      {
        action.point.y *= -1.0f;
      }
    }
  }

  [System.Serializable]
  public class FPSKeyBindings
  {
    public KeyCode forwards = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode turnLeft = KeyCode.Q;
    public KeyCode turnRight = KeyCode.E;
    public KeyCode jump = KeyCode.Space;
  }
}