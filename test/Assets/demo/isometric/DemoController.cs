using System.Linq;
using N.Package.Input;
using N.Package.Input.Controllers;
using N.Package.Input.Motion;
using N.Package.Input.Templates.Isometric;
using UnityEngine;
using UnityEngine.iOS;

namespace Demo.Isometric
{
  public class DemoController : IsoController<DemoBindings>
  {
    protected override void AttachKeyBindings()
    {
      if (Keys.Keyboard.Active)
      {
        BindKeyCode(MotionType.Forwards, Keys.Keyboard.Forwards);
        BindKeyCode(MotionType.Backwards, Keys.Keyboard.Backwards);
        BindKeyCode(MotionType.Left, Keys.Keyboard.Left);
        BindKeyCode(MotionType.Right, Keys.Keyboard.Right);
        BindKeyCode(MotionType.Jump, Keys.Keyboard.Jump);
        BindActionCode(DemoActions.Action, Keys.Keyboard.Action);
      }
      else if (Keys.DualShock3.Active)
      {
        // Get the controller id we can use
        var controllers = Controllers.Enable();
        var controller = controllers.Active.FirstOrDefault();
        if (controller == null)
        {
          _.Log("Unable to find a controller device to bind to. No input configured.");
          return;
        }

        BindActionCode(DemoActions.Action, Keys.DualShock3.Action, controller);
      }
    }

    private void BindActionCode<TAction>(TAction action, DualShock3Inputs input, BaseController controller)
    {
      var onStart = new IsoActionEvent<TAction>(action, true);
      var onStop = new IsoActionEvent<TAction>(action, false);
      Binding.Bind(new DualShock3ButtonBinding<IsoAction>(input, controller, onStart, onStop));
    }

    private void BindAxis<TAction>(TAction action, DualShock3Inputs input, BaseController controller)
    {
    }
  }

  [System.Serializable]
  public class DemoBindings
  {
    public DemoKeyBindings Keyboard;
    public DemoControllerBindings DualShock3;
  }

  [System.Serializable]
  public class DemoKeyBindings
  {
    public bool Active = true;
    public KeyCode Forwards = KeyCode.W;
    public KeyCode Backwards = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Jump = KeyCode.Q;
    public KeyCode Action = KeyCode.E;
  }

  [System.Serializable]
  public class DemoControllerBindings
  {
    public bool Active = false;
    public DualShock3Inputs Movement = DualShock3Inputs.LeftStick;
    public DualShock3Inputs Jump = DualShock3Inputs.Cross;
    public DualShock3Inputs Action = DualShock3Inputs.Square;
  }
}