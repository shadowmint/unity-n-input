using UnityEngine;

namespace N.Package.Input.Motion
{
  public interface IGenericMotionState
  {
    GenericMotionValue GetDirection();
    bool GetFalling();
    bool GetJumping();
  }
}