using UnityEngine;

namespace N.Package.Input.Motion
{
  /// Config settings for a generic motion instance.
  [System.Serializable]
  public class GenericMotionConfig
  {
    [Tooltip("The 'forwards', etc. directions are taken from this object. For FPS style, set it to the actor")]
    public GameObject Directions;

    // The linear speed config.
    public float MaxSpeed = 5f;
    public float Acceleration= 100f;

    // The fixed initial velocity to set when jumping.
    public float JumpSpeed = 3f;

    // The minimum time between jumps
    public float MinJumpInterval = 0.2f;

    // If travelling this fast or faster down, we are 'falling'
    public float JumpFallingThreshold = -10f;

    // Ground detection settings
    public bool EnableGroundDetection = true;
    public float GroundDetectionDistance = 1f;
    public GameObject GroundDetectionPoint;
    public int GroundCollisionMask = -1;

    // Special cases
    [Tooltip("Set to false to allow objects to override kinematic state")]
    public bool ForceObjecToBeNonKinematic = true;

    public Vector3 Forward(Rigidbody body)
    {
      return BestTransform(body).forward;
    }
    
    public Vector3 Forward(Rigidbody2D body)
    {
      return BestTransform(body).forward;
    }

    public Vector3 Right(Rigidbody body)
    {
      return BestTransform(body).right;
    }

    public Vector3 Right(Rigidbody2D body)
    {
      return BestTransform(body).right;
    }
    
    public Vector3 Up(Rigidbody body)
    {
      return BestTransform(body).up;
    }
    
    public Vector3 Up(Rigidbody2D body)
    {
      return BestTransform(body).up;
    }

    private Transform BestTransform(Rigidbody body)
    {
      return Directions == null ? body.transform : Directions.transform;
    }
    
    private Transform BestTransform(Rigidbody2D body)
    {
      return Directions == null ? body.transform : Directions.transform;
    }
  }
}