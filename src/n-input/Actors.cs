using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using N.Package.Input.Events;
using UnityEngine;
using EventHandler = N.Package.Events.EventHandler;
using Object = UnityEngine.Object;

namespace N.Package.Input
{
  /// Actors is a sort of simple high level interface to all actors.
  public class Actors : MonoBehaviour
  {
    public List<Actor> Active = new List<Actor>();

    private static GameObject _actors;

    private readonly EventHandler _eventHandler = new EventHandler();

    private static Actors Instance(bool dontSpawn = false)
    {
      if (_actors == null)
      {
        return dontSpawn ? null : GetInstance();
      }
      try
      {
        return _actors.GetComponent<Actors>();
      }
      catch (Exception)
      {
        return dontSpawn ? null : GetInstance();
      }
    }

    private static Actors GetInstance()
    {
      _actors = new GameObject();
      _actors.transform.name = typeof(Actors).FullName;
      return _actors.AddComponent<Actors>();
    }

    public static void ActorLifecycle(Actor actor, ActorLifecycle status)
    {
      if (status == Events.ActorLifecycle.Created)
      {
        Instance().Active.Add(actor);
        EventHandler.Trigger(new ActorLifecycleEvent()
        {
          Actor = actor,
          Status = status
        });
      }
      else if (status == Events.ActorLifecycle.Destroyed)
      {
        var instance = Instance(true);
        if (instance != null)
        {
          instance.Active.RemoveAll(i => i == actor);
          EventHandler.Trigger(new ActorLifecycleEvent()
          {
            Actor = actor,
            Status = status
          });
        }
      }
    }

    public static EventHandler EventHandler
    {
      get { return Instance()._eventHandler; }
    }

    public void Destroy()
    {
      if (_actors == null) return;
      Object.Destroy(_actors);
    }

    private void OnDestroy()
    {
      _actors = null;
    }
  }
}