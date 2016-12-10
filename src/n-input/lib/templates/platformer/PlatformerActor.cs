using N.Package.Core;
using UnityEngine;

namespace N.Package.Input.Templates.Platformer
{
  /**
     * Basic FPS Actor component.
     * Requires an object layout like:
     *
     *    - Avatar (PlatformActor, Rigidbody, Collider)
     *      - Head
     *      - Body
     */

  [RequireComponent(typeof(Rigidbody2D))]
  public class PlatformerActor : Actor
  {
    [Tooltip("The object to use as this avatar's head")]
    public GameObject Head;

    /// The current motion state
    public PlatformerMotionState Motion;

    /// The rigid body
    private Rigidbody2D _rbody;

    public void Start()
    {
      _rbody = GetComponent<Rigidbody2D>();
    }

    /// Execute an action on this actor
    public override void Trigger<TAction>(TAction action)
    {
      Move(action as PlatformerMoveEvent);
    }

    /// Deal with move actions
    public void Move(PlatformerMoveEvent data)
    {
      if (data == null) return;

      // Jumping
      if ((data.Code == PlatformerMotion.Jump) && (data.Active))
      {
        Motion.VerticalMotion = PlatformerMotion.Jump;
      }
      else if ((data.Code == PlatformerMotion.Jump) && (!data.Active) && (Motion.VerticalMotion == PlatformerMotion.Jump))
      {
        Motion.VerticalMotion = PlatformerMotion.Idle;
      }

      // Left / right
      if ((data.Code == PlatformerMotion.Right) && (data.Active))
      {
        Motion.LateralMotion = PlatformerMotion.Right;
      }
      else if ((data.Code == PlatformerMotion.Right) && (!data.Active) && (Motion.LateralMotion == PlatformerMotion.Right))
      {
        Motion.LateralMotion = PlatformerMotion.Idle;
      }
      else if ((data.Code == PlatformerMotion.Left) && (data.Active))
      {
        Motion.LateralMotion = PlatformerMotion.Left;
      }
      else if ((data.Code == PlatformerMotion.Left) && (!data.Active) && (Motion.LateralMotion == PlatformerMotion.Left))
      {
        Motion.LateralMotion = PlatformerMotion.Idle;
      }
    }

    public override void Update()
    {
      TriggerPending<PlatformerAction>();
      Motion.Update(_rbody);
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.blue;
      foreach (var origin in Motion.RaycastOrigins(transform))
      {
        Gizmos.DrawLine(origin, origin + -transform.up * Motion.FallingRaycastDistance);
      }
    }
  }
}