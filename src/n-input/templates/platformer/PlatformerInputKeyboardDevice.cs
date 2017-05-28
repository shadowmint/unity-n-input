using UnityEngine;

namespace N.Package.Input.Templates.Platformer
{
  public class PlatformerInputKeyboardDevice : PlatformerInputDevice
  {
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Up = KeyCode.W;
    public KeyCode Down = KeyCode.S;
    public KeyCode Jump = KeyCode.Space;

    private bool _stillJumping;

    public void Update()
    {
      if (!Active) return;
      if (UnityEngine.Input.GetKey(Up))
      {
        State.Vertical = 1f;
      }
      else if (UnityEngine.Input.GetKey(Down))
      {
        State.Vertical = -1f;
      }
      else
      {
        State.Vertical = 0f;
      }

      if (UnityEngine.Input.GetKey(Left))
      {
        State.Horizontal = -1f;
      }
      else if (UnityEngine.Input.GetKey(Right))
      {
        State.Horizontal = 1f;
      }
      else
      {
        State.Horizontal = 0f;
      }

      if (UnityEngine.Input.GetKeyDown(Jump))
      {
        if (!_stillJumping)
        {
          State.Jump = true;
        }
      }
      else if (UnityEngine.Input.GetKeyUp(Jump))
      {
        State.Jump = false;
        _stillJumping = false;
      }
    }
  }
}