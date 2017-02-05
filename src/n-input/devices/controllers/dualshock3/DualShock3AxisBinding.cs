using System.Collections.Generic;
using System.Configuration;
using JetBrains.Annotations;
using N.Packages.Input;
using UnityEngine;

namespace N.Package.Input.Controllers
{
  public interface IAxisAction
  {
    Vector2 Axis { set; }
  }

  public class DualShock3AxisBinding<TAction> : IBinding<TAction> where TAction : IAxisAction
  {
    private readonly DualShock3Inputs _key;
    private readonly TAction _change;
    private BaseController _controller;

    public DualShock3AxisBinding(DualShock3Inputs key, TAction change, BaseController controller)
    {
      _key = key;
      _change = change;
      _controller = controller;
    }

    public IEnumerable<TAction> Actions(IInput input)
    {
      var axis = input as InputAxis2D;
      if (axis == null) yield break;
      if (axis.DeviceId != _controller.DeviceId) yield break;
      _change.Axis = axis.GetValue();

      // ... ?
    }
  }
}