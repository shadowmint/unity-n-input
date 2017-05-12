using UnityEngine;

namespace N.Package.Input
{
  /// VirtualDevice is a common abstraction for all input points for a particular actor type.
  public abstract class VirtualDevice : MonoBehaviour
  {
    [Tooltip("Is this input currently enabled?")]
    public bool Active;
  }
}