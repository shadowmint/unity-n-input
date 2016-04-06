namespace N.Package.Input.Next
{
    /// An input instace
    /// eg. Key, Mouse cursor, Move cursor, Video stream, Touch cursor, Audio Stream
    public interface IInput
    {
        /// Return some unique id for this instance
        /// eg. Finger id for multitouch
        int Id { get; }
    }
}
