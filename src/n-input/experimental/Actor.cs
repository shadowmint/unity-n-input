using N.Package.Events;
using UnityEngine;

namespace N.Package.Input.Experimental
{
  public class Actor : MonoBehaviour
  {
    private readonly EventHandler _eventHandler = new EventHandler();

    public EventHandler EventHandler
    {
      get { return _eventHandler; }
    }
  }
}