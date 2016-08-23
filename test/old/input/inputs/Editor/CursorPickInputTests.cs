#if N_INPUT_TESTS
using N;
using N.Package.Input;
using NUnit.Framework;
using UnityEngine;

public class CursorPickTests
{
    [Test]
    public void test_pick_target()
    {
        CursorPickInput.Enable();
        RawInput.Default.Events.AddEventHandler<CursorPickEvent>((ev) =>
        {
            // ...
        });
    }
}
#endif
