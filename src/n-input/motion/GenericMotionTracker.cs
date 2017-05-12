using System;
using UnityEngine;
using EventHandler = N.Package.Events.EventHandler;

namespace N.Package.Input.Motion
{
  /// GenericMotionTracker tracks events and fires events actor when motion changes.
  public class GenericMotionTracker
  {
    private readonly GenericMotion _motion;
    private readonly EventHandler _eventHandler;
    private GenericMotionValue _direction;
    private bool _isFalling;
    private bool _isJumping;

    private const float MinValueThreshold = 0.01f;

    public GenericMotionTracker(GenericMotion motion, EventHandler eventHandler)
    {
      _motion = motion;
      _eventHandler = eventHandler;
      _direction = new GenericMotionValue();
    }

    public void Update(Rigidbody body, GenericMotionConfig config)
    {
      var sendEvent = false;
      var direction = _motion.State.Direction;
      var isJumping = _motion.State.Jumping;
      var isFalling = _motion.State.Falling;

      // Check if we need to send an event?
      if ((_isFalling && !isFalling) || (!_isFalling && isFalling))
      {
        sendEvent = true;
        _isFalling = isFalling;
      }
      else if ((_isJumping && !isJumping) || (!_isJumping && isJumping))
      {
        sendEvent = true;
        _isJumping = isJumping;
      }
      else
      {
        var v = SignOf(direction.Vertical);
        var vo = SignOf(_direction.Vertical);
        var h = SignOf(direction.Horizontal);
        var ho = SignOf(_direction.Horizontal);
        if ((v != vo) || (h != ho))
        {
          sendEvent = true;
          _direction = direction.Clone();
        }
      }

      // Send event
      if (sendEvent)
      {
        _eventHandler.Trigger(new GenericMotionEvent()
        {
          IsFalling = isFalling,
          IsJumping = isJumping,
          Direction = direction.AsVector(body, config)
        });
      }
    }

    public void Track(Action<GenericMotionEvent> onMotionChange)
    {
      _eventHandler.AddEventHandler<GenericMotionEvent>(onMotionChange);
    }

    private SignType SignOf(float value)
    {
      if (value > MinValueThreshold)
      {
        return SignType.Plus;
      }
      if (value < -MinValueThreshold)
      {
        return SignType.Minus;
      }
      return SignType.Zero;
    }
  }

  internal enum SignType
  {
    Zero,
    Plus,
    Minus,
  }
}