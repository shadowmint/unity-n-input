using UnityEngine;
using N.Package.Controller.Input;
using N;

namespace N.Package.Controller {

  // Looks after all the bindings on an object
  [AddComponentMenu("")]
  public class ControllerConfig : MonoBehaviour {

    [Tooltip("The GameObject context for this set of inputs")]
    public GameObject context;

    [Tooltip("The qualified name of the enum these events are associated with, eg. Game.Inputs")]
    public string qualifiedName;

    [Tooltip("The player ID associated with this controller")]
    public int playerId;
  }
}
