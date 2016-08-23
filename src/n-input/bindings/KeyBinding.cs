using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input
{
  /// Bind a key press to an action
  public class KeyBinding<TAction> : IBinding<TAction>
  {
    private KeyCode key;
    private TAction down;
    private TAction up;

    public KeyBinding(KeyCode key, TAction down, TAction up)
    {
      this.key = key;
      this.down = down;
      this.up = up;
    }

    public IEnumerable<TAction> Actions(IInput input)
    {
      var keys = input as Keys;
      if (keys != null)
      {
        if (keys.down(key))
        {
          yield return down;
        }
        else if (keys.up(key))
        {
          yield return up;
        }
      }
    }
  }
}