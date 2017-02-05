using N.Package.Input.Experimental.Events;
using UnityEngine;

namespace N.Package.Input.Experimental
{
  /// The controller is responsible for assigning the IInputDevice that an actor should be using.
  public abstract class Controller : MonoBehaviour
  {
    [Tooltip("Assign an actor here to bind this controller to it")]
    public Actor Actor;

    private Actor _actor;
    private IInput _input;

    public void Update()
    {
      if (_actor != Actor)
      {
        DetachActor();
        AttachActor(Actor);
      }
      if (_input != Input)
      {
        BindInputToActor();
      }
    }

    private void BindInputToActor()
    {
      if (_actor == null) return;
      _input = Input;
      _actor.EventHandler.Trigger(new InputChangedEvent() {Input = Input});
    }

    private void AttachActor(Actor actor)
    {
      if (actor == null) return;
      _actor = actor;
      _actor.EventHandler.Trigger(new ControllerChangedEvent() {Controller = this});
    }

    private void DetachActor()
    {
      if (_actor == null) return;
      _actor.EventHandler.Trigger(new ControllerChangedEvent() {Controller = null});
      _actor = null;
      _input = null;
    }

    protected abstract IInput Input { get; }
  }
}