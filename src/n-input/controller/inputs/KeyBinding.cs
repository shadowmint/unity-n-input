using UnityEngine;
using N;
using N.Package.Controller;
using N.Package.Input;

namespace N.Package.Controller.Input
{
    /// This is the configuration for a single player's controller
    [AddComponentMenu("N/Controller/Key Binding")]
    [RequireComponent(typeof(ControllerConfig))]
    public class KeyBinding : InputHandler
    {

        [Tooltip("The key to associate with this event when a keyboard is available")]
        public KeyCode key;

        public void Start()
        {
            var id = GetComponent<ControllerConfig>().playerId;
            KeyInput.Enable();
            KeyInput.Key(key);

            // Key down
            RawInput.Default.Events.AddEventHandler<KeyDownEvent>((ev) =>
            {
                if (ev.keycode == key)
                {
                    RawInput.Default.Events.Trigger(new ControllerEvent()
                    {
                        active = true,
                        id = eventId,
                        playerId = id
                    });
                }
            });

            // Key up
            RawInput.Default.Events.AddEventHandler<KeyUpEvent>((evp) =>
            {
                if (evp.keycode == key)
                {
                    RawInput.Default.Events.Trigger(new ControllerEvent()
                    {
                        active = false,
                        id = eventId,
                        playerId = id
                    });
                }
            });
        }
    }
}
