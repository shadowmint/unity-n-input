using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Next.Templates.FPS
{
    public class FPSController : Controller
    {
        [Tooltip("The FPS Camera we're using, if any")]
        public new FPSCamera cameraFPS;

        [Tooltip("Invert look")]
        public bool invertLook = false;

        // Event mapper
        private Binding<FPSAction> binding;

        public void Start()
        {
            // Devices
            var mouse = Devices.Mouse;
            mouse.EnableNormalizedCursor2(UnityEngine.Camera.main);
            Inputs.Default.Register(mouse);

            // Inputs
            binding = new Binding<FPSAction>(Inputs.Default);
            binding.Bind(new Cursor2Binding<FPSAction>(new Vector2(0f, 0f), new Vector2(1f, 1f), null, null, new FPSLookAtEvent()));
        }

        /// When an actor is bound, bind the FPS camera to it
        public override void OnActorAttached(Actor actor)
        {
            if (cameraFPS != null)
            {
                cameraFPS.target = (actor as FPSActor).head;
            }
        }

        public override IEnumerable<TAction> Actions<TAction>()
        {
            if (typeof(TAction) == typeof(FPSAction))
            {
                foreach (var action in binding.Actions())
                {
                    InvertLook(action as FPSLookAtEvent);
                    yield return (TAction)(object)action;
                }
            }
        }

        /// Invert action if required
        private void InvertLook(FPSLookAtEvent action)
        {
            if ((action != null) && (invertLook))
            {
                action.point.y *= -1.0f;
            }
        }
    }
}
