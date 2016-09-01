using UnityEngine;

namespace N.Package.Input
{
  /// API to check for the state of various key codes
  public class Keys : IInput
  {
    private readonly int _id;

    public int Id
    {
      get { return _id; }
    }

    public IDevice Device { get; set; }

    public Keys(int id)
    {
      this._id = id;
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