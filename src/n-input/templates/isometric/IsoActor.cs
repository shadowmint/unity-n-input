using N.Package.Input.Events;
using N.Package.Input.Motion;
using UnityEngine;

namespace N.Package.Input.Templates.Isometric
{
  [RequireComponent(typeof(Rigidbody))]
  public class IsoActor : Actor
  {
    public GenericMotion Motion;

    private IsoInputDevice _device;

    private Rigidbody _body;

    private readonly GenericMotionValue _value = new GenericMotionValue();

    public new void Awake()
    {
      base.Awake();
      _body = GetComponent<Rigidbody>();
      EventHandler.AddEventHandler<DeviceChangedEvent>((ep) => { _device = ep.Device as IsoInputDevice; });
      Motion.Tracker.Track(FaceDirectionOfMovement);
    }

    public void Update()
    {
      if (_device == null) return;

      // Next motion displayState
      _value.Horizontal = _device.State.Horizontal;
      _value.Vertical = _device.State.Vertical;
      Motion.State.Jumping = _device.State.Jump;
      _device.State.Jump = false;

      // Update motion displayState
      Motion.Motion(_value);
      Motion.Update(_body);
    }

    private void FaceDirectionOfMovement(GenericMotionEvent ep)
    {
      if (ep.Direction.magnitude > 0.1f)
      {
        var rotation = Quaternion.LookRotation(ep.Direction, Vector3.up);
        transform.rotation = rotation;
        _body.angularVelocity = Vector3.zero;
      }
    }
  }
}