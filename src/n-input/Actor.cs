using UnityEngine;
using System.Collections.Generic;

namespace N.Package.Input
{
  /// An input binding that maps input events into game actions.
  /// When implementing this class, remember to call `TriggerPending` in update.
  public abstract class Actor : MonoBehaviour
  {
    [Tooltip("The currently assigned controller type, if any")]
    public Controller Controller;

    /// The last controller we assigned events to
    private Controller _last;

    /// Execute an action on this actor
    public abstract void Trigger<TAction>(TAction action);

    /// Process any actions on the actor that are pending
    protected void TriggerPending<TAction>()
    {
      if (Controller == null) return;

      if (Controller.Actor != this)
      {
        Controller.Bind(this);
      }

      if (_last != Controller)
      {
        _last = Controller;
        Controller.OnActorAttached(this);
      }

      foreach (var action in Controller.Actions<TAction>())
      {
        Trigger(action);
      }
    }

    /// Override this
    public abstract void Update();
  }
}