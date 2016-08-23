using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input
{
  /// Bind a cursor region to an action
  public class Cursor2Binding<TAction> : IBinding<TAction>
  {
    private TAction enter;
    private TAction leave;
    private TAction motion;
    private Rect bounds;
    private bool active;

    /// Create a new cursor 2 binding
    /// The bottom left corner is -1,-1 and the top right is 1,1.
    /// So for example, 'any look up' might be (-1,0) -> (1,1)
    /// @param min The minimum edge of this region.
    /// @param max The maximum edge of this region.
    /// @param enter The event to emit when entering the zone.
    /// @param leave The event to emit when leaving the zone.
    /// @param motion The event every frame if the position changes.
    public Cursor2Binding(Vector2 min, Vector2 max, TAction enter, TAction leave, TAction motion)
    {
      this.bounds = new Rect(min, max - min);
      this.enter = enter;
      this.leave = leave;
      this.motion = motion;
      active = false;
    }

    public IEnumerable<TAction> Actions(IInput input)
    {
      var cursor = input as Cursor2;
      if (cursor != null)
      {
        yield return motion;
        var point = cursor.Position;
        if (!active && bounds.Contains(point))
        {
          active = true;
          yield return enter;
        }
        if (active && !bounds.Contains(point))
        {
          active = false;
          yield return leave;
        }
      }
    }
  }
}