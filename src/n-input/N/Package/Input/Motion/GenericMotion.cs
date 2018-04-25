using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using N.Package.Events;
using UnityEngine;
using EventHandler = N.Package.Events.EventHandler;

namespace N.Package.Input.Motion
{
  [System.Serializable]
  public class GenericMotion : IGenericMotion
  {
    public GenericMotionConfig Config;
    
    public GenericMotionState State;
    
    private GenericMotionTracker _tracker;
    
    private readonly EventHandler _events = new EventHandler();
    
    private bool _initialized;

    public GenericMotionTracker Tracker
    {
      get { return _tracker ?? (_tracker = new GenericMotionTracker(this, _events)); }
    }

    public void Motion(GenericMotionValue value)
    {
      State.Direction.Horizontal = value.Horizontal;
      State.Direction.Vertical = value.Vertical;
    }

    public void Update(Rigidbody body)
    {
      Configure(body);
      State.Update(Config, body);
      State.Apply(body);
      UpdateTracker(body);
    }

    public IGenericMotionState GetState()
    {
      return State;
    }
    
    // Update the motion tracker; the tracker watches for large changes in motion and notifies.
    // eg. change in direction.
    private void UpdateTracker(Rigidbody body)
    {
      if (_tracker != null)
      {
        _tracker.Update(body, Config);
      }
    }

    // Perform all every frame checks
    private void Configure(Rigidbody body)
    {
      if (Config.ForceObjecToBeNonKinematic && body.isKinematic)
      {
        body.isKinematic = false;
      }
      if (_initialized) return;
      Initialize(body);
    }

    // Perform any one-time init actions
    private void Initialize(Rigidbody body)
    {
      _initialized = true;
      if (Config.Directions != null) return;
      var root = body.gameObject.transform;
      while (root.parent != null)
      {
        root = root.parent;
      }
      Debug.Log("Warning: No direction set for GenericMotion; selecting default: {0}", root);
      Config.Directions = root.gameObject;
    }
  }
}