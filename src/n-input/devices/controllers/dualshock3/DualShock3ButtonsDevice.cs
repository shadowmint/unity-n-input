using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Controllers
{
  public class DualShock3ButtonsDevice : BaseDeviceButtons
  {
    private readonly Dictionary<DualShock3Inputs, KeyCode> _codeMap = new Dictionary<DualShock3Inputs, KeyCode>();

    public DualShock3ButtonsDevice(int joystickId, int inputId, int deviceId) : base(joystickId, inputId, deviceId)
    {
      _codeMap.Add(DualShock3Inputs.Select, CodeFor(0));
      _codeMap.Add(DualShock3Inputs.LeftStick, CodeFor(1));
      _codeMap.Add(DualShock3Inputs.RightStick, CodeFor(2));
      _codeMap.Add(DualShock3Inputs.Start, CodeFor(3));
      _codeMap.Add(DualShock3Inputs.UpDPad, CodeFor(4));
      _codeMap.Add(DualShock3Inputs.RightDPad, CodeFor(5));
      _codeMap.Add(DualShock3Inputs.DownDPad, CodeFor(6));
      _codeMap.Add(DualShock3Inputs.LeftDPad, CodeFor(7));
      _codeMap.Add(DualShock3Inputs.L2, CodeFor(8));
      _codeMap.Add(DualShock3Inputs.R2, CodeFor(9));
      _codeMap.Add(DualShock3Inputs.L1, CodeFor(10));
      _codeMap.Add(DualShock3Inputs.R1, CodeFor(11));
      _codeMap.Add(DualShock3Inputs.Triangle, CodeFor(12));
      _codeMap.Add(DualShock3Inputs.Circle, CodeFor(13));
      _codeMap.Add(DualShock3Inputs.Cross, CodeFor(14));
      _codeMap.Add(DualShock3Inputs.Square, CodeFor(15));
      _codeMap.Add(DualShock3Inputs.PsButtom, CodeFor(16));
    }

    protected override bool MapCode<T>(T value, out KeyCode code)
    {
      code = KeyCode.Break;
      if (typeof(T) != typeof(DualShock3Inputs)) return false;
      var button = (DualShock3Inputs) (object) value;
      if (!_codeMap.ContainsKey(button)) return false;
      code = _codeMap[button];
      return true;
    }
  }
}