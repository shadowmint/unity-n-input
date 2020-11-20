using UnityEngine;

namespace N.Package.Input.Example
{
    [System.Serializable]
    public class ExamplePlayerInputState
    {
        public Vector3 move;
        public Vector3 look;
        public bool jump;
    }
}