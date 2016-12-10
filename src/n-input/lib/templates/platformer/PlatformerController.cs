using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Templates.Platformer
{
  public class PlatformerController : Controller
  {
    [Tooltip("The Platformer Camera we're using, if any")]
    public PlatformerCamera PlatformerCamera;

    [Tooltip("Keybindings")]
    public PlatformerKeyBindings KeyBindings = new PlatformerKeyBindings();

    // Event mapper
    private Binding<PlatformerAction> _binding;

    public void Start()
    {
      // Devices
      Inputs.Default.Register(Devices.Keyboard);

      // Inputs: Keyboard
      _binding = new Binding<PlatformerAction>(Inputs.Default);
      _binding.Bind(new KeyBinding<PlatformerAction>(KeyBindings.Left, new PlatformerMoveEvent(PlatformerMotion.Left, true), new PlatformerMoveEvent(PlatformerMotion.Left, false)));
      _binding.Bind(new KeyBinding<PlatformerAction>(KeyBindings.Right, new PlatformerMoveEvent(PlatformerMotion.Right, true), new PlatformerMoveEvent(PlatformerMotion.Right, false)));
      _binding.Bind(new KeyBinding<PlatformerAction>(KeyBindings.Jump, new PlatformerMoveEvent(PlatformerMotion.Jump, true), new PlatformerMoveEvent(PlatformerMotion.Jump, false)));
    }

    /// When an actor is bound, bind the camera to it
    public override void OnActorAttached(Actor actor)
    {
      if (PlatformerCamera != null)
      {
        PlatformerCamera.Target = (actor as PlatformerActor).Head;
      }
    }

    public override IEnumerable<TAction> Actions<TAction>()
    {
      if (typeof(TAction) != typeof(PlatformerAction)) yield break;
      foreach (var action in _binding.Actions())
      {
        yield return (TAction) (object) action;
      }
    }
  }
}