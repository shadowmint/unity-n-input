using N.Package.Input.Infrastructure;

namespace N.Package.Input
{
    public abstract class InputActor<TState> : InputInternalActorType where TState : class
    {
        public void Update()
        {
            if (!ShouldProcessInput) return;
            var state = Controller.GetState<TState>();
            if (state == null) return;
            UpdateFromInput(state);
        }

        protected abstract void UpdateFromInput(TState state);
    }
}