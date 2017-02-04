using N.Package.Input.Motion;
using N.Package.Input.Templates.Isometric;
using UnityEngine;

namespace Demo.Isometric
{
  public class DemoController : IsoController<DemoKeyBindings>
  {
    protected override void AttachKeyBindings()
    {
      BindKeyCode(MotionType.Forwards, Keys.Forwards);
      BindKeyCode(MotionType.Backwards, Keys.Backwards);
      BindKeyCode(MotionType.Left, Keys.Left);
      BindKeyCode(MotionType.Right, Keys.Right);
      BindKeyCode(MotionType.Jump, Keys.Jump);
      BindActionCode(DemoActions.Action, Keys.Action);
    }
  }

  [System.Serializable]
  public class DemoKeyBindings
  {
    public KeyCode Forwards = KeyCode.W;
    public KeyCode Backwards = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Jump = KeyCode.Q;
    public KeyCode Action = KeyCode.E;
  }
}