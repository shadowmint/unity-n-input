# n-input

Low level input semantics & high level controller helpers.

Typically prefer to use a high level controller rather than the low
level scripts.

## Usage

See the tests in the `Editor/` folder for each class for usage examples.

### Controller

Add a `KeyBinding` item to some controller object.

Setup the `ControllerConfig` with an enum:

      public enum Foo {
        LEFT,
        RIGHT
      }

You must provide the fully qualified name for this; eg. `MyApp.Foo`

Finally, in your script, use the RawInput handler to listen for `ControllerEvents`:

    public enum Inputs {
      LEFT,
      RIGHT
    }

    public class Input : MonoBehaviour {
      public void Start() {
        RawInput.Default.Events.AddEventHandler<ControllerEvent>((ep) => {
          if (evp.Is<Inputs>(Inputs.LEFT)) {
            // Start turning left...
          }
          else if (evp.Is<Inputs>(Inputs.RIGHT)) {
            // Start turning right...
          }
        });
      }
    }

### Input

1) Add a 'Raw Input Listener' component to the scene.

2) To add a custom event target, extend `IRawEventHandler`:

    public class FakeInputHandler : IRawInputHandler {
      public int count = 0;
      public void UpdateFrame(EventHandler ev) {}
      public void Update(EventHandler ev) {
        count += 1;
        if (count == 2) {
          ev.Trigger(new FakeInputEvent());
        }
      }
    }

3) Then register and bind it:

    var handler = new FakeInputHandler();
    RawInput.Default.Register(handler);
    RawInput.Default.Events.AddEventHandler<FakeInputEvent>((ev) => {
      // ...
    });

#### Existing input bindings

See the examples in inputs/; each one should have a test case
at the bottom on how to use them.

Typically the usage is like this:

    KeyInput.Enable();
    RawInput.Event((ev) => { ... });

However, notice that some Enable() calls may take specific configuration,
and have additional apis to control their specific behaviour.

To remove an input:

    KeyInput.Disable();

## Install

From your unity project folder:

    npm init
    npm install shadowmint/unity-n-input --save
    echo Assets/packages >> .gitignore
    echo Assets/packages.meta >> .gitignore

The package and all its dependencies will be installed in
your Assets/packages folder.

## Development

Setup and run tests:

    npm install
    npm install ..
    cd test
    npm install
    gulp

Remember that changes made to the test folder are not saved to the package
unless they are copied back into the source folder.

To reinstall the files from the src folder, run `npm install ..` again.

### Tests

All tests are wrapped in `#if ...` blocks to prevent test spam.

You can enable tests in: Player settings > Other Settings > Scripting Define Symbols

The test key for this package is: N_INPUT_TESTS
