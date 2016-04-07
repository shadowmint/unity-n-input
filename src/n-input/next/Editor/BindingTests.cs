#if N_INPUT_TESTS
using UnityEngine;
using N.Package.Input.Next;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class BindingTests : N.Tests.Test
{
    public enum FakeAction
    {
        FAKE1,
        FAKE2
    }

    public class FakeBinding : IBinding<FakeAction>
    {
        public IEnumerable<FakeAction> Actions(IInput input)
        {
            var fake = input as DevicesTests.FakeInput;
            if (fake != null)
            {
                if (fake.Id == 0)
                {
                    yield return FakeAction.FAKE1;
                }
                else
                {
                    yield return FakeAction.FAKE2;
                }
            }
        }
    }

    [Test]
    public void test_create_fake_binding()
    {
        var input = new Inputs();
        input.Register(new DevicesTests.FakeDevice());

        var binding = new Binding<FakeAction>(input);
        binding.Bind(new FakeBinding());

        var actions = binding.Actions().ToArray();
        Assert(actions.Length > 0);
    }
}
#endif
