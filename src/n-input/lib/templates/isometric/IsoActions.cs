using N.Package.Input.Motion;
using UnityEngine;

namespace N.Package.Input.Templates.Isometric
{
  /// Action base type
  public abstract class IsoAction : IAction
  {
    public abstract void Configure(IInput input);
  }

  /// Move event type
  public class IsoMoveEvent : IsoAction
  {
    /// What action is this?
    public MotionType Code;

    /// Is this action active
    public bool Active;

    public IsoMoveEvent(MotionType code, bool active)
    {
      Code = code;
      Active = active;
    }

    public override void Configure(IInput input)
    {
    }
  }

  /// Action event type (eg. use)
  public class IsoActionEvent<TAction> : IsoAction
  {
    /// What action is this?
    public TAction Action;

    /// Is this action active
    public bool Active;

    public IsoActionEvent(TAction action, bool active)
    {
      Action = action;
      Active = active;
    }

    public override void Configure(IInput input)
    {
    }
  }
}