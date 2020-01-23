using N.Package.Input.Infrastructure;
using UnityEngine;

namespace N.Package.Input
{
    public class InputPlayer : MonoBehaviour
    {
        public InputInternalActorType actor;
        public InputDevice device;

        private readonly InputController _controller = new InputController();

        private InputInternalActorType _actor;
        private InputDevice _device;

        public void Update()
        {
            if ((_actor == actor) && (_device == device)) return;
            _actor = actor;
            _device = device;
            _controller.Connect(_actor);
            _controller.Connect(_device);
        }
    }
}