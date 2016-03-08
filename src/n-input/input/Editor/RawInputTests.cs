#if N_INPUT_TESTS
using UnityEngine;
using N;
using N.Package.Input;
using N.Package.Events;
using System.Collections.Generic;
using NUnit.Framework;

public class FakeInputEvent : IEvent
{
    public IEventApi Api { get; set; }
}

public class FakeInputHandler : IRawInputHandler
{
    public int count = 0;
    public void UpdateFrame(EventHandler ev)
    {
    }
    public void Update(EventHandler ev)
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
        RawInput.Default.Clear();

        var count = 0;
        var handler = new FakeInputHandler();
        RawInput.Default.Register(handler);
        RawInput.Default.Events.AddEventHandler<FakeInputEvent>((ev) =>
        {
            count += 1;
        });

        RawInput.Default.Update();
        Assert(handler.count == 1);
        Assert(count == 0);

        RawInput.Default.Update();
        Assert(handler.count == 2);
        Assert(count == 1);
    }
}
#endif
