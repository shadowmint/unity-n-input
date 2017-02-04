using N.Package.Input.Motion;
using UnityEngine;

namespace N.Package.Input.Templates.Isometric
{
  public class SampleIsoActor : IsoActor<SampleIsoActionTypes>
  {
    public override void Action(IsoActionEvent<SampleIsoActionTypes> data)
    {
    }

    public override void OnMotionChange(GenericMotionEvent ep)
    {
      if (ep.Direction.magnitude > 0.1f)
      {
        var rotation = Quaternion.LookRotation(ep.Direction, Vector3.up);
        transform.rotation = rotation;
        Rbody.angularVelocity = Vector3.zero;
      }
    }
  }
}