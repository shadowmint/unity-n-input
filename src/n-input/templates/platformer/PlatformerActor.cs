using N.Package.Input.Events;
using N.Package.Input.Motion;
using UnityEngine;

namespace N.Package.Input.Templates.Platformer
{
  [RequireComponent(typeof(Rigidbody2D))]
  public class PlatformerActor : Actor
  {
    public GenericMotion2D Motion;

    [Tooltip("Bind the display object of the player here if needed")]
    public GameObject Display;

    private PlatformerInputDevice _device;

    private Rigidbody2D _body;

    private readonly GenericMotionValue _value = new GenericMotionValue();

    public new void Awake()
    {
      base.Awake();
      _body = GetComponent<Rigidbody2D>();
      EventHandler.AddEventHandler<DeviceChangedEvent>((ep) => { _device = ep.Device as PlatformerInputDevice; });
      Motion.Tracker.Track(FaceDirectionOfMovement);
    }

    public void Update()
    {
      if (_device == null) return;

      // Next motion state
      _value.Horizontal = _device.State.Horizontal;
      _value.Vertical = _device.State.Vertical;
      Motion.State.Jumping = _device.State.Jump;
      _device.State.Jump = false;

      // Update motion state
      Motion.Motion(_value);
      Motion.Update(_body, Camera);
    }

    private void FaceDirectionOfMovement(GenericMotionEvent ep)
    {
      if (Display == null) return;
      if (ep.Direction.magnitude > 0.1f)
      {
        var rotation = Quaternion.LookRotation(ep.Direction, Vector3.up);
        Display.transform.rotation = rotation;
        _body.angularVelocity = 0f;
      }
    }
  }
}