using UnityEngine;

namespace N.Package.Input.Motion
{
  /// Range of -1 to 1 for each axis, to support gamepads.
  [System.Serializable]
  public class GenericMotionValue
  {
    [Range(-1f, 1f)]
    public float Vertical;

    [Range(-1f, 1f)]
    public float Horizontal;

    [Range(0f, 1f)]
    public float Jump = 1.0f;

    public void Add(GenericMotionValue value)
    {
      Vertical = Mathf.Clamp(Vertical + value.Vertical, -1f, 1f);
      Horizontal = Mathf.Clamp(Horizontal + value.Horizontal, -1f, 1f);
      Jump = Mathf.Clamp(Jump + value.Jump, 0f, 1f);
    }

    public float Magnitude()
    {
      return Mathf.Max(Mathf.Abs(Vertical), Mathf.Abs(Horizontal));
    }

    public Vector3 AsVector(Rigidbody body, GenericMotionConfig config)
    {
      return (config.Forward(body) * Vertical + config.Right(body) * Horizontal).normalized;
    }

    public GenericMotionValue Clone()
    {
      return new GenericMotionValue()
      {
        Vertical = Vertical,
        Horizontal = Horizontal,
        Jump = Jump
      };
    }
  }
}