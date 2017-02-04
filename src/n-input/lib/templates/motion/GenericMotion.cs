using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using N.Package.Events;
using UnityEngine;
using EventHandler = N.Package.Events.EventHandler;

namespace N.Package.Input.Motion
{
  [System.Serializable]
  public class GenericMotion
  {
    public GenericMotionConfig Config;
    public GenericMotionState State;
    private GenericMotionTracker _tracker;
    private readonly EventHandler _events = new EventHandler();
    private bool _initialized;

    public GenericMotionTracker Tracker
    {
      get { return _tracker ?? (_tracker = new GenericMotionTracker(this, _events)); }
      set { _tracker = value; }
    }

    /// Autogenerate a motion value from a cardinal direction.
    public void Motion(MotionType motion, bool active)
    {
      var sign = active ? 1.0f : -1.0f;
      switch (motion)
      {
        case MotionType.Forwards:
          Motion(new GenericMotionValue() {Vertical = 1.0f * sign});
          break;
        case MotionType.Backwards:
          Motion(new GenericMotionValue() {Vertical = -1.0f * sign});
          break;
        case MotionType.Left:
          Motion(new GenericMotionValue() {Horizontal = -1.0f * sign});
          break;
        case MotionType.Right:
          Motion(new GenericMotionValue() {Horizontal = 1.0f * sign});
          break;
        case MotionType.Jump:
          State.Jumping = active;
          break;
      }
    }

    public void Motion(GenericMotionValue value)
    {
        State.Direction.Add(value);
    }

    public void Update(Rigidbody body)
    {
      Configure(body);
      State.Update(Config, body);
      State.Apply(body);
      UpdateTracker(body);
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
      if (body.isKinematic)
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
      _.Log("Warning: No direction set for GenericMotion; selecting default: {0}", root);
      Config.Directions = root.gameObject;
    }
  }
}