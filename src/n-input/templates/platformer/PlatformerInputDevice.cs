using UnityEngine;

namespace N.Package.Input.Templates.Platformer
{
  [System.Serializable]
  public struct PlatformerInputDeviceState
  {
    [Range(-1, 1)]
    public float Vertical;

    [Range(-1, 1)]
    public float Horizontal;

    public bool Jump;
  }

  public class PlatformerInputDevice : VirtualDevice
  {
    public PlatformerInputDeviceState State;
  }
}