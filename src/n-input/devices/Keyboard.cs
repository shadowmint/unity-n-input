using System.Collections.Generic;

namespace N.Package.Input
{
  public class Keyboard : IDevice
  {
    /// Keys api
    private Keys keys;

    public Keyboard()
    {
      keys = new Keys(0);
    }

    /// The set of inputs on this device
    public IEnumerable<IInput> Inputs
    {
      get { yield return keys; }
    }
  }
}