using System.Collections.Generic;
using System.Linq;
using N.Package.Core;
using N.Package.Input.Events;
using UnityEngine;

namespace N.Package.Input.Templates.Isometric
{
  [System.Serializable]
  public struct IsoCameraItem
  {
    public Controller Controller;
    public Actor Actor;
  }

  public class IsoCamera : MonoBehaviour
  {
    public Vector3 Offset;

    [Tooltip("Set the value to make the camera move further away from the actors the further they get from each other")]
    [Range(0, 5)]
    public float DistanceFactor;

    public List<IsoCameraItem> Actors;

    public void Awake()
    {
      Input.Actors.EventHandler.AddEventHandler<ActorLifecycleEvent>((ep) =>
      {
        if (ep.Status == ActorLifecycle.Destroyed)
        {
          Release(ep.Actor);
        }
      });

      Input.Actors.EventHandler.AddEventHandler<ControllerChangedEvent>((ep) =>
      {
        if (ep.Controller == null)
        {
          Release(ep.Actor);
        }
        else
        {
          Bind(ep.Controller, ep.Actor);
        }
      });
    }

    public void Bind(Controller controller, Actor actor)
    {
      Release(actor);
      Actors.Add(new IsoCameraItem()
      {
        Controller = controller,
        Actor = actor
      });
      Offset = RecalculateOffset();
    }

    public void Release(Actor actor)
    {
      Actors.RemoveAll(i => i.Actor == actor);
    }

    private Vector3 AverageActorPosition()
    {
      var sum = Actors.Aggregate(Vector3.zero, (acc, i) => acc + i.Actor.transform.position);
      return sum / (float) Actors.Count;
    }

    private float AverageActorPositionDelta()
    {
      var total = Actors.Sum(a1 => Actors.Sum(
        a2 => Vector3.Distance(a1.Actor.transform.position, a2.Actor.transform.position)));
      return total / (float) (Actors.Count * Actors.Count);
    }

    private Vector3 RecalculateOffset()
    {
      return transform.position - AverageActorPosition();
    }

    public void Update()
    {
      if (Actors.Count > 0)
      {
        var offset = Offset + AverageActorPositionDelta() * -1.0f * DistanceFactor * transform.forward;
        transform.position = AverageActorPosition() + offset;
      }
    }
  }
}