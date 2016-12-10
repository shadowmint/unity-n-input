using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;

namespace N.Package.Input.Templates.Platformer
{
  /// Motion state
  [System.Serializable]
  public class PlatformerMotionState
  {
    public PlatformerMotion LateralMotion;
    public PlatformerMotion VerticalMotion;

    [Tooltip("The left/right speed for the actor")]
    public float Speed;

    [Tooltip("Vertical velocity applied when jumping")]
    public float JumpSpeed;

    [Tooltip("Is this actor currently on solid ground?")]
    public bool IsFalling = false;

    [Tooltip("Jump detect distance")]
    public float FallingRaycastDistance = 1f;

    [Tooltip("Edge offset for jump detect distance")]
    public float FallingRaycastOffset = 0.2f;

    [Tooltip("Time to go from no speed to max speed")]
    public float TimeToMaxSpeed = 0.3f;

    [Tooltip("Ground layer mask")]
    public int GroundLayerId = 8;

    /// Never allow falls to be shorter than this (ie. bounces)
    private const float MinAirTime = 0.5f;

    /// How long have we been falling?
    private float _airTime = 0f;

    /// How long has a particular direction arrow been held down?
    private float _directionTime = 0;

    /// What was the last lateral motion we saw?
    private PlatformerMotion _lastLateralMotion;

    /// Yield a set of raycast origins
    public IEnumerable<Vector3> RaycastOrigins(Transform transform)
    {
      var offset = transform.right * FallingRaycastOffset / 2.0f;
      yield return transform.position;
      yield return transform.position - offset;
      yield return transform.position + offset;
    }

    public void UpdateFallingState(Rigidbody2D body)
    {
      var found = false;
      foreach (var origin in RaycastOrigins(body.transform))
      {
        var hit = Physics2D.Raycast(origin, -body.transform.up, FallingRaycastDistance,1 << GroundLayerId);
        if (hit.collider == null) continue; // Next raycast

        found = true;
        _airTime += Time.deltaTime;
        if (!(_airTime > MinAirTime)) continue;

        _airTime = 0f;
        IsFalling = false;
      }

      // Now falling~
      if (found) return;
      if (!IsFalling)
      {
        _airTime = 0f;
      }
      IsFalling = true;
    }

    public void Update(Rigidbody2D body)
    {
      if (body == null) return;

      // Update the time spent moving
      if (_lastLateralMotion != LateralMotion)
      {
        _lastLateralMotion = LateralMotion;
        _directionTime = 0f;
      }
      else
      {
        _directionTime += Time.deltaTime;
      }

      // Calculate the fractional speed time
      var speedFraction = Mathf.Clamp(_directionTime, 0f, TimeToMaxSpeed) / TimeToMaxSpeed;

      // Jumping
      UpdateFallingState(body);
      if ((VerticalMotion == PlatformerMotion.Jump) && (!IsFalling))
      {
        SetVelocityComponent(body, body.transform.up * JumpSpeed, body.transform.up);
        IsFalling = true;
      }

      // Left / right
      switch (LateralMotion)
      {
        case PlatformerMotion.Right:
          SetVelocityComponent(body, body.transform.right * Speed * speedFraction, body.transform.right);
          break;
        case PlatformerMotion.Left:
          SetVelocityComponent(body, -body.transform.right * Speed * speedFraction, body.transform.right);
          break;
        case PlatformerMotion.Idle:
          if (!IsFalling)
          {
            SetVelocityComponent(body, Vector3.zero, body.transform.right);
          }
          break;
      }
    }

    private void SetVelocityComponent(Rigidbody2D body, Vector3 speed, Vector3 direction)
    {
      var oldComponent = Vector3.Project(body.velocity, direction);
      var newComponent = Vector3.Project(speed, direction);
      var oldVelocity = body.velocity;
      body.velocity = oldVelocity - (Vector2) oldComponent + (Vector2) newComponent;
    }
  }
}