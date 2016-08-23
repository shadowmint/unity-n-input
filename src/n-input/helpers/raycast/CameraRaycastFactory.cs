using UnityEngine;
using System.Collections.Generic;
using UE = UnityEngine;

namespace N.Package.Input.Helpers
{
  /// A raycast factory to store in various parent types
  public class CameraRaycastFactory : IRaycastFactory
  {
    /// Camera to raycast from
    public UE.Camera Camera;

    /// Return the distance
    public float Distance { get; set; }

    /// Return the cast filter
    public int LayerMask { get; set; }

    /// The internally cached ray object
    public Ray Ray { get; set; }

    /// Set and get the total number of hits
    public int Count { get; set; }

    /// Is this a 2D or 3D camera?
    public bool UseRaycast2D
    {
      get { return Camera.orthographic; }
    }

    /// Last update
    private int lastUpdate = 0;

    /// Update origin if required
    public void Update(Cursor2 cursor)
    {
      if (lastUpdate != Time.frameCount)
      {
        lastUpdate = Time.frameCount;
        Ray = Camera.ScreenPointToRay(cursor.Position);
      }
    }
  }
}