using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace N.Package.Input.Motion
{
  [RequireComponent(typeof(Camera))]
  public class InputShiftableCamera : MonoBehaviour
  {
    public float Value = 0.0f;
    public Vector3 Min = Vector3.zero;
    public Vector3 Max = Vector3.zero;
    public Vector3 Offset;
    public float Speed = 0.1f;

    private Vector3 _target;

    private float _value;

    private const float Threshold = 0.001f;
    private const float FloatThreshold = 0.01f;

    public void Update()
    {
      UpdateOffset();
      UpdatePosition();
    }

    private void UpdatePosition()
    {
      var delta = (_target - Offset);
      if (delta.magnitude < FloatThreshold)
      {
        Offset = _target;
        return;
      }

      var direction = delta.normalized;
      var step = direction * Speed * Time.deltaTime;
      Offset += step;
    }

    private void UpdateOffset()
    {
      if (Math.Abs(Value) < Threshold)
      {
        _value = 0f;
        _target = Vector3.zero;
        return;
      }

      if (Math.Abs(_value - Value) < Threshold) return;

      var value = Mathf.Clamp(Value, -1.0f, 1.0f);
      var relativeValue = ((1.0f + value) / 2.0f);
      _target = Vector3.Lerp(Min, Max, relativeValue);
    }
  }
}