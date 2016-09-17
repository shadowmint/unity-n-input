using N.Package.Core;
using UnityEngine;

namespace N.Package.Input.Templates.FPS
{
  /**
     * Basic FPS Actor component.
     * Requires an object layout like:
     *
     *    - Avatar (FPSActor, Rigidbody, Collider)
     *      - Head
     *      - Body
     */

  [RequireComponent(typeof(Rigidbody))]
  public class FPSActor : Actor
  {
    [Tooltip("Maximum left right look extents")]
    [Range(45f, 110f)]
    public float maxLookLeftRight = 90f;

    [Tooltip("Maximum up down look extents")]
    [Range(45f, 110f)]
    public float maxLookUpDown = 90f;

    [Tooltip("The speed this actor moves at")]
    public float speed = 1f;

    [Tooltip("The object to use as this avatar's head")]
    public GameObject head;

    [Tooltip("The object which is the visible body of the avatar")]
    public GameObject body;

    /// The rigid body
    private Rigidbody rbody;

    /// The current motion state
    public FPSMotionState motion;

    public void Start()
    {
      rbody = GetComponent<Rigidbody>();
    }

    /// Execute an action on this actor
    public override void Trigger<TAction>(TAction action)
    {
      LookAt(action as FPSLookAtEvent);
      Move(action as FPSMoveEvent);
    }

    /// Deal with look actions
    public void LookAt(FPSLookAtEvent data)
    {
      if (data != null)
      {
        var y = Mathf.Clamp(data.point.y*-90f, -maxLookUpDown, maxLookUpDown);
        var x = Mathf.Clamp(data.point.x*90f, -maxLookLeftRight, maxLookLeftRight);
        head.SetRotation(new Vector3(y, x, 0f));
        body.SetRotation(new Vector3(0f, x, 0f));
        motion.SyncMotionToLook(head, rbody);
      }
    }

    /// Deal with move actions
    public void Move(FPSMoveEvent data)
    {
      if (data != null)
      {
        // Jumping
        if ((data.code == FPSMotion.JUMP) && (data.active))
        {
          motion.verticalMotion = FPSMotion.JUMP;
        }
        else if ((data.code == FPSMotion.JUMP) && (!data.active) && (motion.verticalMotion == FPSMotion.JUMP))
        {
          motion.verticalMotion = FPSMotion.IDLE;
        }

        // Forwards backwards
        if ((data.code == FPSMotion.FORWARDS) && (data.active))
        {
          motion.motion = FPSMotion.FORWARDS;
        }
        else if ((data.code == FPSMotion.FORWARDS) && (!data.active) && (motion.motion == FPSMotion.FORWARDS))
        {
          motion.motion = FPSMotion.IDLE;
        }
        else if ((data.code == FPSMotion.BACKWARDS) && (data.active))
        {
          motion.motion = FPSMotion.BACKWARDS;
        }
        else if ((data.code == FPSMotion.BACKWARDS) && (!data.active) && (motion.motion == FPSMotion.BACKWARDS))
        {
          motion.motion = FPSMotion.IDLE;
        }

        // Left / right
        if ((data.code == FPSMotion.RIGHT) && (data.active))
        {
          motion.lateralMotion = FPSMotion.RIGHT;
        }
        else if ((data.code == FPSMotion.RIGHT) && (!data.active) && (motion.lateralMotion == FPSMotion.RIGHT))
        {
          motion.lateralMotion = FPSMotion.IDLE;
        }
        else if ((data.code == FPSMotion.LEFT) && (data.active))
        {
          motion.lateralMotion = FPSMotion.LEFT;
        }
        else if ((data.code == FPSMotion.LEFT) && (!data.active) && (motion.lateralMotion == FPSMotion.LEFT))
        {
          motion.lateralMotion = FPSMotion.IDLE;
        }

        // Turn left / right
        if ((data.code == FPSMotion.TURN_RIGHT) && (data.active))
        {
          motion.turn = FPSMotion.TURN_RIGHT;
        }
        else if ((data.code == FPSMotion.TURN_RIGHT) && (!data.active) && (motion.turn == FPSMotion.TURN_RIGHT))
        {
          motion.turn = FPSMotion.IDLE;
        }
        else if ((data.code == FPSMotion.TURN_LEFT) && (data.active))
        {
          motion.turn = FPSMotion.TURN_LEFT;
        }
        else if ((data.code == FPSMotion.TURN_LEFT) && (!data.active) && (motion.turn == FPSMotion.TURN_LEFT))
        {
          motion.turn = FPSMotion.IDLE;
        }
      }
    }

    public override void Update()
    {
      TriggerPending<FPSAction>();
      motion.Update(rbody);
    }
  }
}