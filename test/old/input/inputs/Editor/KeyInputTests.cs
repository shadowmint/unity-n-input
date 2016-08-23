#if N_INPUT_TESTS
using N;
using N.Package.Input;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class KeyInputTests
{
    [Test]
    public void test_key_input()
    {
        KeyInput.Enable();
        KeyInput.Key(KeyCode.Space);
        RawInput.Default.Events.AddEventHandler<KeyDownEvent>((ev) =>
        {
            // ...
        });
    }
}
#endif
