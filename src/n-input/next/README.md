# Input.Next

Devices is a list of devices.

Inputs is a stream of input types abstracted from various devices.

## Controllers and Binding

Create a custom event type TEvent.

Extend Controller for the custom controller instance.

Extend Actor for the custom Actor type.

Each controller should be able to have an active binding, possibly with
other bindings which can be swapped in and out.

To bind a game object to a controller, assign the controller to the
Actor object.

### Event types

The event type TAction which is assigned to a binding can be anything;
but if it *does* implement IAction, it will be passed the input to `Configure`
to configure itself.

### Controllers

Controllers are invoked by the Actor when the Actor calls 'TriggerPending()';
this should be done in the Update loop.

The broad over view of the event flow is as follows:

- Actor Update() calls TriggerPending()
- The controller passes the event stream to the active Binding.
- Any events are fed back into the Trigger() call on the Actor.

However, because of generic limitations, the scaffolding for this has
to be done manually.

The first step is an event type:

```
public enum MyEventType
{
    START_TURN_LEFT,
    STOP_TURN_LEFT,
    START_TURN_RIGHT,
    STOP_TURN_RIGHT,
    START_FORWARDS,
    STOP_FORWARDS,
}
```

Now define a controller that takes a Binding<MyEventType> and generates a
stream of event objects:

```
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
```

Finally, define an actor that uses these events:

```
public class BoxActor : Actor
{
    public bool left;
    public bool right;
    public bool forward;

    public float speed;
    public float maxSpeed;

    public float spin;

    public float distance;

    /// Execute an action on this actor
    public override void Trigger<TAction>(TAction action)
    {
        switch ((MyEventType)(object)action)
        {
            case MyEventType.START_FORWARDS:
                forward = true;
                break;
            case MyEventType.STOP_FORWARDS:
                forward = false;
                break;
            case MyEventType.START_TURN_LEFT:
                left = true;
                break;
            case MyEventType.STOP_TURN_LEFT:
                left = false;
                break;
            case MyEventType.START_TURN_RIGHT:
                right = true;
                break;
            case MyEventType.STOP_TURN_RIGHT:
                right = false;
                break;
        }
    }

    public void Update()
    {
        // Trigger events
        this.TriggerPending<MyEventType>();

        /// Other things...
    }
}
```
