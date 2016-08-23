using UnityEngine;

namespace N.Package.Input
{
  /// API to check for the state of various key codes
  public class Keys : IInput
  {
    private int id;

    public int Id
    {
      get { return id; }
    }

    public Keys(int id)
    {
      this.id = id;
    }

    public bool down(KeyCode key)
    {
      return UnityEngine.Input.GetKeyDown(key);
    }

    public bool up(KeyCode key)
    {
      return UnityEngine.Input.GetKeyUp(key);
    }
  }
}