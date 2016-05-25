using UnityEngine;

namespace N.Package.Input.Next
{
    /// API for mouse position
    public class Cursor2 : IInput
    {
        private int id;
        public int Id { get { return id; } }

        /// Screen position
        public virtual Vector2 Position
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

    /// API for mouse position normalized to -1,-1 -> 1,1
    /// with -1,-1 as bottom left, and 1,1 as top right.
    public class NCursor2 : Cursor2
    {
        /// The camera to use for this binding
        public UnityEngine.Camera camera;

        /// Screen position
        public override Vector2 Position
        {
            get
            {
                var halfWidth = camera.pixelWidth / 2f;
                var halfHeight = camera.pixelHeight / 2f;
                var real = UnityEngine.Input.mousePosition;
                var x = Mathf.Clamp((real[0] - halfWidth) / halfWidth, -1f, 1f);
                var y = Mathf.Clamp((real[1] - halfHeight) / halfHeight, -1f, 1f);
                return new Vector2(x, y);
            }
        }

        public NCursor2(int id, UnityEngine.Camera camera) : base(id)
        {
            this.camera = camera;
        }
    }
}
