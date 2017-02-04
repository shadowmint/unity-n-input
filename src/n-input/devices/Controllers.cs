using System.Collections.Generic;
using System.Linq;

namespace N.Package.Input.Controllers
{
  public class Controllers : IDevice
  {
    private static Controllers _instance;

    private static Controllers Instance
    {
      get { return _instance ?? (_instance = new Controllers()); }
    }

    public static Controllers Enable()
    {
      N.Package.Input.Inputs.Default.Register(Instance);
      return Instance;
    }

    private readonly List<BaseController> _controllers = new List<BaseController>();

    public Controllers()
    {
      var names = UnityEngine.Input.GetJoystickNames();
      for (var i = 0; i < names.Length; i++)
      {
        if (DualShock3.Matches(names[i]))
        {
          _controllers.Add(new DualShock3(i, names[i]));
        }
      }
    }

    public IEnumerable<IInput> Inputs
    {
      get { return _controllers.SelectMany(controller => controller.Inputs); }
    }

    public IEnumerable<BaseController> Active
    {
      get { return _controllers; }
    }
  }
}