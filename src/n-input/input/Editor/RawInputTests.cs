#if N_INPUT_TESTS
using UnityEngine;
using N;
using N.Package.Input;
using System.Collections.Generic;
using NUnit.Framework;

public class FakeInputEvent : N.Event
{
}

public class FakeInputHandler : RawInputHandler
{
    public int count = 0;
    public void UpdateFrame(Events ev)
    {
    }
    public void Update(Events ev)
    {
        count += 1;
        if (count == 2)
        {
            ev.Trigger(new FakeInputEvent());
        }
    }
}

public class RawInputTests : N.Tests.Test
{
    [Test]
    public void test_custom_handler()
    {
        RawInput.Clear();

        var count = 0;
        var handler = new FakeInputHandler();
        RawInput.Register(handler);
        RawInput.Event((ev) =>
        {
            if ((ev as FakeInputEvent) != null)
            {
                count += 1;
            }
        });

        RawInput.Update();
        Assert(handler.count == 1);
        Assert(count == 0);

        RawInput.Update();
        Assert(handler.count == 2);
        Assert(count == 1);
    }
}
#endif
