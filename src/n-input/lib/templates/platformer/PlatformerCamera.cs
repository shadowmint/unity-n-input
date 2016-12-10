using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input.Templates.Platformer
{
  [RequireComponent(typeof(UnityEngine.Camera))]
  public class PlatformerCamera : MonoBehaviour
  {
    [Tooltip("Target this object")]
    public GameObject Target;

    private Vector3 _offset;

    private bool _initialized;

    public void Update()
    {
      if ((!_initialized) && (Target != null))
      {
        _offset = gameObject.transform.position - Target.transform.position;
        _initialized = true;
      }
      else if (Target == null)
      {
        _initialized = false;
      }
    }

    public void LateUpdate()
    {
      if (Target != null)
      {
        transform.position = Target.transform.position + _offset;
      }
    }
  }
}