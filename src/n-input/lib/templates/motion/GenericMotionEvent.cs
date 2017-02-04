using UnityEngine;

namespace N.Package.Input.Motion
{
  public class GenericMotionEvent
  {
    public bool IsFalling { get; set; }
    public bool IsJumping { get; set; }
    public Vector3 Direction { get; set; }
  }
}