namespace N.Package.Input.Next
{
    /// Implement this on a TAction for a binding to allow it to be configured.
    public interface IAction
    {
        /// Allow this action to receive the input state from some input object.
        /// For example, for a Cursor2, the event may want the actual Vector.
        void Configure(IInput input);
    }
}
