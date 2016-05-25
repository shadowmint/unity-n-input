using UnityEngine;

namespace N.Package.Input.Next.Templates.FPS
{
    /// Action base type
    public abstract class FPSAction : IAction
    {
        public abstract void Configure(IInput input);
    }

    /// Look in a particular direction
    public class FPSLookAtEvent : FPSAction
    {
        public Vector2 point;

        public override void Configure(IInput input)
        {
            if (input is NCursor2)
            {
                point = (input as NCursor2).Position;
            }
        }
    }
}
