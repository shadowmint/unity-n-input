using N.Package.Input.Experimental.Events;
using N.Package.Input.Motion;
using UnityEngine;

namespace N.Package.Input.Templates.Isometric.Experimental
{
  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(Collider))]
  public class ExperimentalIsoActor : Input.Experimental.Actor
  {
    public GenericMotion ActorMotion;

    /// The rigid body
    protected Rigidbody Rbody;

    /// The current input api
    private ExperimentalIsoInput _input;

    /// The current motion state
    public void Start()
    {
      Rbody = GetComponent<Rigidbody>();
      ActorMotion.Tracker.Track(OnMotionChange);
      EventHandler.AddEventHandler<InputChangedEvent>((ep) => { _input = ep.Input as ExperimentalIsoInput; });
    }

    public void Update()
    {
      if (_input == null) return;
      ActorMotion.Motion(new GenericMotionValue()
      {
        Jump = _input.Jump ? 1.0f : 0.0f,
        Horizontal = _input.Horizontal,
        Vertical = _input.Vertical
      });
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

    /// Deal with direction changes and animations
    public abstract void OnMotionChange(GenericMotionEvent ep);
  }
}