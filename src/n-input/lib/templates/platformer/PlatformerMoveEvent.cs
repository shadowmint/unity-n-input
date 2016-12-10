using N.Package.Input.Templates.Platformer;

namespace N.Package.Input.Templates.Platformer
{
  /// Move event type
  public class PlatformerMoveEvent : PlatformerAction
  {
    /// What action is this?
    public PlatformerMotion Code;

    /// Is this action active
    public bool Active;

    public PlatformerMoveEvent(PlatformerMotion code, bool active)
    {
      this.Code = code;
      this.Active = active;
    }

    public override void Configure(IInput input)
    {
    }
  }
}