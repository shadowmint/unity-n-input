using UnityEngine;

namespace N.Package.Input.Templates.Platformer
{
  /// Action base type
  public abstract class PlatformerAction : IAction
  {
    public abstract void Configure(IInput input);
  }
}