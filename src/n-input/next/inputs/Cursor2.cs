using UnityEngine;

namespace N.Package.Input.Next
{
    /// API for mouse position
    public class Cursor2 : IInput
    {
        private int id;
        public int Id { get { return id; } }

        /// Screen position
        public Vector2 Position
        {
            get
            {
                return UnityEngine.Input.mousePosition;
            }
        }

        public Cursor2(int id)
        {
            this.id = id;
        }
    }
}
