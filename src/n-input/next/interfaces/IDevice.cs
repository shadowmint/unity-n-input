using System.Collections.Generic;

namespace N.Package.Input.Next
{
    /// An input device
    /// eg. Keyboard, move controller, mouse, camera, touch surface
    public interface IDevice
    {
        /// The set of inputs on this device
        IEnumerable<IInput> Inputs { get; }
    }
}
