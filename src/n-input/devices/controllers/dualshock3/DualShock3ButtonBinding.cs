using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Controllers
{
  public class DualShock3ButtonBinding<TAction> : IBinding<TAction>
  {
    private readonly DualShock3Inputs _key;
    private readonly TAction _down;
    private readonly TAction _up;
    private readonly BaseController _controller;

    public DualShock3ButtonBinding(DualShock3Inputs key, BaseController controller, TAction down, TAction up)
    {
      _key = key;
      _down = down;
      _up = up;
      _controller = controller;
    }

    public IEnumerable<TAction> Actions(IInput input)
    {
      var keys = input as DualShock3ButtonsDevice;
      if (keys == null) yield break;
      if (keys.DeviceId != _controller.DeviceId) yield break;
      if (keys.Down(_key))
      {
        yield return _down;
      }
      else if (keys.Up(_key))
      {
        yield return _up;
      }
    }
  }
}