using System.Collections.Generic;

namespace N.Package.Input.Next
{
    /// High level input API
    public class Inputs
    {
        /// The default instance
        private static Inputs inputs = null;
        public static Inputs Default
        {
            get
            {
                if (inputs == null)
                {
                    inputs = new Inputs();
                }
                return inputs;
            }
        }

        /// Set of devices
        private IList<IDevice> devices = new List<IDevice>();

        /// Register a device with this input manager
        public void Register(IDevice device)
        {
            if (!devices.Contains(device))
            {
                devices.Add(device);
            }
        }

        /// Remove a device with this input manager


        /// Enumerate devices

        /// Yield all inputs
        public IEnumerable<IInput> Stream()
        {
            foreach (var device in devices)
            {
                foreach (var input in device.Inputs)
                {
                    yield return input;
                }
            }
        }

        /// Yield all inputs from target
        public IEnumerable<TInput> Stream<TInput>() where TInput : class, IInput
        {
            foreach (var device in devices)
            {
                foreach (var input in device.Inputs)
                {
                    if (input is TInput)
                    {
                        yield return input as TInput;
                    }
                }
            }
        }

        /// Yield all inputs that are TInput

        /// Yield all inputs that are TInput from target
    }
}
