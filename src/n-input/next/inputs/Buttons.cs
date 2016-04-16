using UnityEngine;

namespace N.Package.Input.Next
{
    /// A generic buttons interaction api
    public class Buttons : IInput
    {
        private int id;
        public int Id { get { return id; } }

        public Buttons(int id)
        {
            this.id = id;
        }

        public bool down(KeyCode key)
        {
            return UnityEngine.Input.GetKeyDown(key);
        }

        public bool up(KeyCode key)
        {
            return UnityEngine.Input.GetKeyUp(key);
        }
    }
}
