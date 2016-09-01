using UnityEngine;

namespace N.Package.Input
{
  /// A generic buttons interaction api
  public class Buttons : IInput
  {
    private readonly int _id;

    public int Id
    {
      get { return _id; }
    }

    public IDevice Device { get; set; }

    public Buttons(int id)
    {
      _id = id;
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