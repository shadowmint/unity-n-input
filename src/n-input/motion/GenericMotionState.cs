using System.Linq;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using Random = N.Package.Core.Random;

namespace N.Package.Input.Motion
{
  /// The current motion information
  [System.Serializable]
  public class GenericMotionState
  {
    [Range(0, 2)] public float SpeedMultiplier = 1.0f;

    public GenericMotionValue Direction;
    public Vector3 Velocity;
    public Vector3 Impulse;
    public Vector3 Force;
    public bool Falling;
    public bool Jumping;
    public bool Grounded;
    private float _elapsedSinceLastJump = -1f;

    private const float MinimumVelocityTheshold = 0.01f;

    public void Update(GenericMotionConfig config, Rigidbody body)
    {
      UpdateCurrentState(config, body);

      // Flat land control
      var current = Velocity - Vector3.Project(Velocity, config.Up(body));
      var target = Direction.AsVector(body, config) * config.MaxSpeed * SpeedMultiplier * Direction.Magnitude();
      var fallingAccelerationMultiplier = Falling ? 0.0f : 1.0f;
      var delta = (target - current).normalized * config.Acceleration * fallingAccelerationMultiplier * Time.deltaTime;
      Velocity += delta;

      // Convery velocity into force to apply.
      current = body.velocity - Vector3.Project(body.velocity, config.Up(body));
      target = Velocity - current;
      Force = target.magnitude * target.magnitude * target.normalized * body.mass;

      // Jumping
      DetectGround(config, body);
      if (Jumping)
      {
        if (Grounded && !Falling)
        {
          if (_elapsedSinceLastJump > config.MinJumpInterval)
          {
            Impulse = config.Up(body) * Direction.Jump * config.JumpSpeed * body.mass;
            Jumping = false;
            _elapsedSinceLastJump = 0f;
          }
        }
      }
      _elapsedSinceLastJump += Time.deltaTime;

      HaltMinimumVelocities();
    }

    private void HaltMinimumVelocities()
    {
      if (Mathf.Abs(Velocity.x) < MinimumVelocityTheshold)
      {
        Velocity.x = 0f;
      }
      if (Mathf.Abs(Velocity.y) < MinimumVelocityTheshold)
      {
        Velocity.y = 0f;
      }
      if (Mathf.Abs(Velocity.z) < MinimumVelocityTheshold)
      {
        Velocity.z = 0f;
      }
    }

    private void UpdateCurrentState(GenericMotionConfig config, Rigidbody body)
    {
      if (Grounded)
      {
        Falling = false;
      }
      else
      {
        Falling = body.velocity.y < config.JumpFallingThreshold;
      }
      if (Falling)
      {
        Jumping = false;
      }
    }

    private void DetectGround(GenericMotionConfig config, Rigidbody body)
    {
      if (!config.EnableGroundDetection) return;
      var root = config.GroundDetectionPoint != null ? config.GroundDetectionPoint : body.gameObject;
      var hits = Physics.RaycastAll(root.transform.position, -config.Up(body), config.GroundDetectionDistance,
        config.GroundCollisionMask);
      Grounded = hits.Any(i => i.collider.gameObject != body.gameObject);
    }

    public void Apply(Rigidbody body)
    {
      body.AddForce(Force, ForceMode.Force);
      body.AddForce(Impulse, ForceMode.Impulse);
      Impulse = Vector3.zero;
      Force = Vector3.zero;
    }
  }
}