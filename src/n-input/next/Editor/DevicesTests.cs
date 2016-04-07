#if N_INPUT_TESTS
using UnityEngine;
using N.Package.Input.Next;
using System.Collections.Generic;
using NUnit.Framework;

public class DevicesTests : N.Tests.Test
{
    public class FakeInput : IInput
    {
        public int Id { get; set; }
    }

    public class FakeDevice : IDevice
    {
        private IInput[] inputs;
        public FakeDevice()
        {
            inputs = new IInput[]
            {
                new FakeInput() { Id = 0 },
                new FakeInput() { Id = 1 }
            };
        }
        /// The set of inputs on this device
        public IEnumerable<IInput> Inputs
        {
            get
            {

                return inputs;
            }
        }
    }

    [Test]
    public void test_create_fake_binding()
    {

    }

}
#endif
