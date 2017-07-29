using UnityEngine;

namespace N.Package.Input
{
  /// VirtualDevice is a common abstraction for all input points for a particular actor type.
  public abstract class VirtualDevice : MonoBehaviour
  {
    [Tooltip("Is this input currently enabled?")]
    public bool Active;

    /// <summary>
    /// OnActorAttached is invoked when the controller assigned the device to an actor.
    /// </summary>
    public virtual void OnActorAttached(Actor actor)
    {
    }

    /// <summary>
    /// OnActorDetached is invoked when the controller removes the assigned device to actor binding.
    /// </summary>
    public virtual void OnActorDetached()
    {
    }
  }
}