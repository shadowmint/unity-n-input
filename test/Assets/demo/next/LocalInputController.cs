using UnityEngine;
using System.Collections.Generic;
using N.Package.Input.Next;

public class LocalInputController : Controller
{
    public KeyCode left;
    public KeyCode right;
    public KeyCode forward;

    public void Start()
    {
        Inputs.Default.Register(Devices.Keyboard);
    }

    public override IEnumerable<TAction> Actions<TAction>()
    {
        foreach (var keys in Inputs.Default.Stream<Keys>())
        {
            if (keys.down(left))
            {
                yield return (TAction)(object)MyEventType.START_TURN_LEFT;
            }
            if (keys.up(left))
            {
                yield return (TAction)(object)MyEventType.STOP_TURN_LEFT;
            }
            if (keys.down(right))
            {
                yield return (TAction)(object)MyEventType.START_TURN_RIGHT;
            }
            if (keys.up(right))
            {
                yield return (TAction)(object)MyEventType.STOP_TURN_RIGHT;
            }
            if (keys.down(forward))
            {
                yield return (TAction)(object)MyEventType.START_FORWARDS;
            }
            if (keys.up(forward))
            {
                yield return (TAction)(object)MyEventType.STOP_FORWARDS;
            }
        }
    }
}
