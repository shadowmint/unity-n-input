using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input
{
  /// An event stream generates TAction instances from some source.
  /// eg. Local input, remote input, AI
  public abstract class Controller : MonoBehaviour
  {
    [Tooltip("Follow actor position on any axis?")]
    public bool followActorPosition = true;

    [Tooltip("Follow actor rotation on any axis?")]
    public bool followActorRotation = false;

    public Actor Actor
    {
      get { return _actor; }
    }

    /// The actor associated with this controller.
    /// Associate an actor by assigning the controller component on it.
    private Actor _actor;

    /// Generate the next set of input actions
    public abstract IEnumerable<TAction> Actions<TAction>();

    /// Attach an actor to this controller
    public void Bind(Actor actor)
    {
      _actor = actor;
      actor.Controller = this;
      OnActorAttached(actor);
    }

    /// Override this to get notification that a new actor is using this controller
    public virtual void OnActorAttached(Actor actor)
    {
    }

    /// Update position and rotation
    public void Update()
    {
      if (_actor == null) return;

      if (followActorPosition)
      {
        if (transform.position != _actor.transform.position)
        {
          transform.position = _actor.transform.position;
        }
      }

      if (followActorRotation)
      {
        if (transform.rotation != _actor.transform.rotation)
        {
          transform.rotation = _actor.transform.rotation;
        }
      }
    }
  }
}