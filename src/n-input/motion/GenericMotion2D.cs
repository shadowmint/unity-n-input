using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using N.Package.Events;
using UnityEngine;
using EventHandler = N.Package.Events.EventHandler;

namespace N.Package.Input.Motion
{
  [System.Serializable]
  public class GenericMotion2D : IGenericMotion
  {
    public GenericMotionConfig Config;
    public GenericMotionState2D State;
    private GenericMotionTracker _tracker;
    private InputShiftableCamera _shiftCamera;
    private readonly EventHandler _events = new EventHandler();
    private bool _initialized;

    public GenericMotionTracker Tracker
    {
      get { return _tracker ?? (_tracker = new GenericMotionTracker(this, _events)); }
      set { _tracker = value; }
    }

    public void Motion(GenericMotionValue value)
    {
      State.Direction.Horizontal = value.Horizontal;
      State.Direction.Vertical = value.Vertical;
    }

    public void Update(Rigidbody2D body, Camera camera)
    {
      Configure(body, camera);
      UpdateTracker(body);
      State.Update(Config, body);
      State.Apply(_shiftCamera);
      State.Apply(body);
    }

    public IGenericMotionState GetState()
    {
      return State;
    }
    
    // Update the motion tracker; the tracker watches for large changes in motion and notifies.
    // eg. change in direction.
    private void UpdateTracker(Rigidbody2D body)
    {
      if (_tracker != null)
      {
        _tracker.Update(body, Config);
      }
    }

    // Perform all every frame checks
    private void Configure(Rigidbody2D body, Camera camera)
    {
      if (Config.ForceObjecToBeNonKinematic && body.isKinematic)
      {
        body.isKinematic = false;
      }
      if (_initialized) return;
      Initialize(body, camera);
    }

    // Perform any one-time init actions
    private void Initialize(Rigidbody2D body, Camera camera)
    {
      _initialized = true;
      _shiftCamera = camera == null ? null : camera.gameObject.GetComponent<InputShiftableCamera>();
      
      if (Config.Directions != null) return;
      var root = body.gameObject.transform;
      while (root.parent != null)
      {
        root = root.parent;
      }
      _.Log("Warning: No direction set for GenericMotion; selecting default: {0}", root);
      Config.Directions = root.gameObject;
    }
  }
}