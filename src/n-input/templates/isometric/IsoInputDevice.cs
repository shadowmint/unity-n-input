using UnityEngine;

namespace N.Package.Input.Templates.Isometric
{
  [System.Serializable]
  public struct IsoInputDeviceState
  {
    [Range(-1, 1)]
    public float Vertical;

    [Range(-1, 1)]
    public float Horizontal;

    public bool Jump;
  }

  public class IsoInputDevice : VirtualDevice
  {
    public IsoInputDeviceState State;
  }
}