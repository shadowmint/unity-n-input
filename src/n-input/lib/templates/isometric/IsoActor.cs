using N.Package.Core;
using N.Package.Input.Motion;
using N.Package.Input.Templates.FPS;
using UnityEngine;

namespace N.Package.Input.Templates.Isometric
{
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(Collider))]
  public abstract class IsoActor<TAction> : Actor
  {
    public GenericMotion ActorMotion;

    /// The rigid body
    protected Rigidbody Rbody;

    /// The current motion state
    public void Start()
    {
      Rbody = GetComponent<Rigidbody>();
      ActorMotion.Tracker.Track(OnMotionChange);
    }

    /// Execute an action on this actor
    public override void Trigger<TEvent>(TEvent action)
    {
      if (action is IsoMoveEvent)
      {
        Move(action as IsoMoveEvent);
      }
      if (action is IsoActionEvent<TAction>)
      {
        Action(action as IsoActionEvent<TAction>);
      }
    }

    public override void Update()
    {
      TriggerPending<IsoAction>();
    }

    public void FixedUpdate()
    {
      ActorMotion.Update(Rbody);
    }

    /// Deal with move actions
    public void Move(IsoMoveEvent data)
    {
      ActorMotion.Motion(data.Code, data.Active);
    }

    /// Deal with other actions
    public abstract void Action(IsoActionEvent<TAction> data);

    /// Deal with direction changes and animations
    public abstract void OnMotionChange(GenericMotionEvent ep);
  }
}