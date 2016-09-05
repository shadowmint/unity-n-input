using UnityEngine;
using System.Collections.Generic;

namespace N.Package.Input
{
  /// API for world position
  public class Cursor3 : IInput
  {
    protected int id;

    public int Id
    {
      get { return id; }
    }

    /// World position
    public Vector3 Position
    {
      get { return new Vector3(0f, 0f, 0f); }
    }

    public Cursor3(int id)
    {
      this.id = id;
    }
  }
}