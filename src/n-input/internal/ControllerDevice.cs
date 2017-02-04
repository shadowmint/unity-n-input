using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Controllers.Internal
{
  internal class ControllerDevice
  {
    public string Id { get; set; }

    private readonly List<string> _axisNames = new List<string>();
    private readonly List<KeyCode> _buttonNames = new List<KeyCode>();
    private readonly int _id;

    private const int MinInputId = 1;
    private const int MaxInputId = 100;

    public ControllerDevice(int id, string axisTemplate = "Joy{0}Axis{1}")
    {
      _id = id;
      Id = UnityEngine.Input.GetJoystickNames()[id];
      EnumerateAxis(axisTemplate);
      EnumerateButtons();
    }

    private void EnumerateAxis(string axisTemplate)
    {
      for (var i = MinInputId; i < MaxInputId; i++)
      {
        var name = string.Format(axisTemplate, _id + 1, i);
        try
        {
          var value = UnityEngine.Input.GetAxis(name);
          if (Math.Abs(value) > 0.01f)
          {
            _axisNames.Add(name);
          }
        }
        catch (Exception)
        {
          break;
        }
      }
    }

    private void EnumerateButtons()
    {
      for (var i = MinInputId; i < MaxInputId; i++)
      {
        try
        {
          var name = string.Format("Joystick{0}Button{1}", _id + 1, i);
          var code = (KeyCode) Enum.Parse(typeof(KeyCode), name);
          _buttonNames.Add(code);
        }
        catch (Exception)
        {
          break;
        }
      }
    }

    public IEnumerable<InputAxisValue> PollAxis()
    {
      foreach (var name in _axisNames)
      {
        var value = UnityEngine.Input.GetAxis(name);
        if (Math.Abs(value) > 0.01f)
        {
          yield return new InputAxisValue() {Id = name, Value = value};
        }
      }
    }

    public IEnumerable<InputButtonValue> PollButtons()
    {
      foreach (var name in _buttonNames)
      {
        var value = UnityEngine.Input.GetKeyDown(name);
        if (value)
        {
          yield return new InputButtonValue() {Id = name, Value = true};
        }
        else
        {
          value = UnityEngine.Input.GetKeyUp(name);
          if (value)
          {
            yield return new InputButtonValue() {Id = name, Value = false};
          }
        }
      }
    }
  }

  public struct InputAxisValue
  {
    public string Id;
    public float Value;
  }

  public struct InputButtonValue
  {
    public KeyCode Id;
    public bool Value;
  }
}