using UnityEngine;

namespace N.Package.Input.Templates.FPS
{
  /// Action base type
  public abstract class FPSAction : IAction
  {
    public abstract void Configure(IInput input);
  }

  /// Look in a particular direction
  public class FPSLookAtEvent : FPSAction
  {
    public Vector2 point;

    public override void Configure(IInput input)
    {
      if (input is NCursor2)
      {
        point = (input as NCursor2).Position;
      }
    }
  }

  /// Move event type
  public class FPSMoveEvent : FPSAction
  {
    /// What action is this?
    public FPSMotion code;

    /// Is this action active
    public bool active;

    public FPSMoveEvent(FPSMotion code, bool active)
    {
      this.code = code;
      this.active = active;
    }

    public override void Configure(IInput input)
    {
    }
  }
}