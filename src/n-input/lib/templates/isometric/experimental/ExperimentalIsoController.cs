namespace N.Package.Input.Templates.Isometric.Experimental
{
  public class ExperimentalIsoController : Input.Experimental.Controller
  {
    private ExperimentalIsoInput _input;

    protected override Input.Experimental.IInput Input
    {
      get { return _input ?? (_input = new ExperimentalIsoInput()); }
    }
  }
}