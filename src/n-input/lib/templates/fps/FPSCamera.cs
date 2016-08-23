using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Templates.FPS
{
  [RequireComponent(typeof(UnityEngine.Camera))]
  public class FPSCamera : MonoBehaviour
  {
    [Tooltip("Target this object")]
    public GameObject target;

    public void LateUpdate()
    {
      if (target != null)
      {
        transform.rotation = target.transform.rotation;
        transform.position = target.transform.position;
      }
    }
  }
}