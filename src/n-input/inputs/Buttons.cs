using UnityEngine;

namespace N.Package.Input
{
  /// A generic buttons interaction api
  public class Buttons : IInput
  {
    private int id;

    public int Id
    {
      get { return id; }
    }

    public Buttons(int id)
    {
      this.id = id;
    }

    public bool Down(KeyCode key)
    {
      return UnityEngine.Input.GetKeyDown(key);
    }

    public bool Up(KeyCode key)
    {
      return UnityEngine.Input.GetKeyUp(key);
    }
  }
}