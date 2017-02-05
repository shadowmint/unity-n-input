using UnityEngine;

namespace N.Package.Input.Templates.Isometric.Experimental
{
  public class ExperimentalIsoInput : Input.Experimental.IInput
  {
    public bool Jump { get; set; }
    public float Vertical { get; set; }
    public float Horizontal { get; set; }


  }
}