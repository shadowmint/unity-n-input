using UnityEngine;
using System.Collections.Generic;
using N.Package.Input;

public class LocalInputController : Controller
{
    public KeyCode left;
    public KeyCode right;
    public KeyCode forward;
    private Binding<MyEventType> binding;

    public void Start()
    {
        Inputs.Default.Register(Devices.Keyboard);
        binding = new Binding<MyEventType>(Inputs.Default);
        binding.Bind(new KeyBinding<MyEventType>(left, MyEventType.START_TURN_LEFT, MyEventType.STOP_TURN_LEFT));
        binding.Bind(new KeyBinding<MyEventType>(right, MyEventType.START_TURN_RIGHT, MyEventType.STOP_TURN_RIGHT));
        binding.Bind(new KeyBinding<MyEventType>(forward, MyEventType.START_FORWARDS, MyEventType.STOP_FORWARDS));
    }

    public override IEnumerable<TAction> Actions<TAction>()
    {
        if (typeof(TAction) == typeof(MyEventType))
        {
            foreach (var action in binding.Actions())
            {
                yield return (TAction)(object)action;
            }
        }
    }
}
