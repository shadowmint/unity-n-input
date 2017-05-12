using UnityEngine;

namespace N.Package.Input.Templates.Isometric
{
  public class IsoInputKeyboardDevice : IsoInputDevice
  {
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Forward = KeyCode.W;
    public KeyCode Backward = KeyCode.S;
    public KeyCode Jump = KeyCode.Space;

    private bool _stillJumping;

    public void Update()
    {
      if (!Active) return;
      if (UnityEngine.Input.GetKey(Forward))
      {
        State.Vertical = 1f;
      }
      else if (UnityEngine.Input.GetKey(Backward))
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