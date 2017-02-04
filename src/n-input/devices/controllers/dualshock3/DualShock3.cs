using N.Packages.Input;

namespace N.Package.Input.Controllers
{
  public class DualShock3 : BaseController
  {
    private const string AxisX1 = "Joy1Axis1";
    private const string AxisY1 = "Joy1Axis2";
    private const string AxisX2 = "Joy1Axis3";
    private const string AxisY2 = "Joy1Axis4";

    public static bool Matches(string id)
    {
      return id == "Sony PLAYSTATION(R)3 Controller";
    }

    public DualShock3(int joystickId, string deviceName) : base(joystickId, deviceName)
    {
    }

    protected override DeviceButtons DeviceSpecificButtonMap(int inputId, int deviceId)
    {
      return new DualShock3ButtonsDevice(JoystickId, inputId, deviceId);
    }

    protected override void DeviceSpecificAxis()
    {
      AddAxis(new InputAxis2D(AxisX1, AxisY1, Devices.InputId, DeviceId));
      AddAxis(new InputAxis2D(AxisX2, AxisY2, Devices.InputId, DeviceId));
    }
  }
}