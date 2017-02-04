using System.Collections;
using System.Collections.Generic;
using N.Package.Input.Motion;
using N.Package.Input.Templates.Isometric;
using UnityEngine;

namespace Demo.Isometric
{
  public class DemoActor : IsoActor<DemoActions>
  {
    public override void Action(IsoActionEvent<DemoActions> data)
    {
      if (data.Active && data.Action == DemoActions.Action)
      {
        _.Log("ACTION!");
      }
    }

    public override void OnMotionChange(GenericMotionEvent ep)
    {
      if (!(ep.Direction.magnitude > 0.1f)) return;
      var rotation = Quaternion.LookRotation(ep.Direction, Vector3.up);
      transform.rotation = rotation;
      Rbody.angularVelocity = Vector3.zero;
    }
  }
}