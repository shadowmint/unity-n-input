using N.Package.Events;
using N.Package.Input.Events;
using UnityEngine;

namespace N.Package.Input
{
  public class Actor : MonoBehaviour
  {
    private readonly EventHandler _eventHandler = new EventHandler();

    public EventHandler EventHandler
    {
      get { return _eventHandler; }
    }

    public void Awake()
    {
      Actors.ActorLifecycle(this, ActorLifecycle.Created);
    }

    public void OnDestroy()
    {
      Actors.ActorLifecycle(this, ActorLifecycle.Destroyed);
    }
  }
}