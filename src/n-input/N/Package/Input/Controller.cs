using System.Linq;
using N.Package.Input.Events;
using UnityEngine;

namespace N.Package.Input
{
  /// The controller is responsible for assigning the IInputDevice that an actor should be using.
  public class Controller : MonoBehaviour
  {
    [Tooltip("Assign an actor here to bind this controller to it")]
    public Actor Actor;

    [Tooltip("Assign an input here to bind this controller to it")]
    public VirtualDevice Device;

    [Tooltip("A list of all active devices which could be found")]
    public VirtualDevice[] AvailableDevices;

    [Tooltip("Update the list of available devices this often")]
    public float InputScanInterval = 1f;

    private float _elapsed;

    private Actor _actor;

    private VirtualDevice _device;

    public void Awake()
    {
      _elapsed = InputScanInterval + 1f;
      UpdateDeviceList();
    }

    public void Update()
    {
      if (_actor != Actor)
      {
        DetachActor();
        AttachActor(Actor);
      }
      if (_device != Device)
      {
        BindInputToActor();
      }
      UpdateDeviceList();
    }

    private void UpdateDeviceList()
    {
      _elapsed += Time.deltaTime;
      if (_elapsed < InputScanInterval) return;
      _elapsed = 0f;
      AvailableDevices = GetComponentsInChildren<VirtualDevice>().Where(i => i.Active).ToArray();
    }

    private void BindInputToActor()
    {
      if (_actor == null) return;
      if (_device != null)
      {
        _device.OnActorDetached();
      }
      _device = Device;
      _actor.EventHandler.Trigger(new DeviceChangedEvent() {Device = Device});
      _device.OnActorAttached(_actor);
    }

    private void AttachActor(Actor actor)
    {
      if (actor == null) return;
      _actor = actor;
      _actor.EventHandler.Trigger(new ControllerChangedEvent() {Controller = this, Actor = actor});
      Actors.EventHandler.Trigger(new ControllerChangedEvent() {Controller = this, Actor = actor});
    }

    private void DetachActor()
    {
      if (_actor == null) return;
      _actor.EventHandler.Trigger(new ControllerChangedEvent() {Controller = null, Actor = _actor});
      Actors.EventHandler.Trigger(new ControllerChangedEvent() {Controller = this, Actor = _actor});
      _actor = null;
      _device = null;
    }
  }
}