using N.Package.Input.Motion;
using UnityEngine;

namespace N.Package.Input.Templates.Isometric
{
  public class SampleIsoController : IsoController<SampleIsoKeyBindings>
  {
    protected override void AttachKeyBindings()
    {
      BindKeyCode(MotionType.Forwards, Keys.Forwards);
      BindKeyCode(MotionType.Backwards, Keys.Backwards);
      BindKeyCode(MotionType.Left, Keys.Left);
      BindKeyCode(MotionType.Right, Keys.Right);
      BindKeyCode(MotionType.Jump, Keys.Jump);
      BindActionCode(SampleIsoActionTypes.Use, Keys.Use);
    }
  }

  [System.Serializable]
  public class SampleIsoKeyBindings
  {
    public KeyCode Forwards = KeyCode.W;
    public KeyCode Backwards = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Jump = KeyCode.Q;
    public KeyCode Use = KeyCode.E;
  }

  public enum SampleIsoActionTypes
  {
    Use
  }
}