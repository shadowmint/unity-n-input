using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using N.Package.Core;
using N.Package.Input.Motion;
using UnityEngine;

namespace N.Package.Input.Templates.Isometric
{
  public abstract class IsoController<TKeyEvents> : Controller
  {
    [Tooltip("The Camera we're using, if any")]
    public IsoCamera IsoCamera;

    [Tooltip("The keybindings for this controller")]
    public TKeyEvents Keys;

    protected Binding<IsoAction> Binding;

    public void Start()
    {
      // Devices
      Inputs.Default.Register(Devices.Keyboard);

      // Inputs: Keyboard
      Binding = new Binding<IsoAction>(Inputs.Default);
      AttachKeyBindings();

      // Find camera if we cam
      if (IsoCamera == null)
      {
        IsoCamera = Scene.FindComponent<IsoCamera>();
      }
    }

    // Implement key bindings with this
    protected abstract void AttachKeyBindings();

    /// When an actor is bound, bind the camera to it
    public override void OnActorAttached(Actor actor)
    {
      if (IsoCamera != null)
      {
        IsoCamera.Bind(this, actor);
      }
    }

    public override void OnActorDetached()
    {
      if (IsoCamera != null)
      {
        IsoCamera.Release(this);
      }
    }

    public override IEnumerable<TAction> Actions<TAction>()
    {
      if (typeof(TAction) != typeof(IsoAction)) yield break;
      foreach (var action in Binding.Actions())
      {
        yield return (TAction) (object) action;
      }
    }

    // Helper method for implementations
    protected void BindKeyCode(MotionType motion, KeyCode key)
    {
      var onStart = new IsoMoveEvent(motion, true);
      var onStop = new IsoMoveEvent(motion, false);
      Binding.Bind(new KeyBinding<IsoAction>(key, onStart, onStop));
    }

    // Helper method for implementations
    protected void BindActionCode<TAction>(TAction action, KeyCode key)
    {
      var onStart = new IsoActionEvent<TAction>(action, true);
      var onStop = new IsoActionEvent<TAction>(action, false);
      Binding.Bind(new KeyBinding<IsoAction>(key, onStart, onStop));
    }
  }
}