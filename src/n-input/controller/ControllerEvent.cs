using N.Reflect;
using System.Collections.Generic;
using System;

namespace N.Package.Controller {

  /// A pick event
  public class ControllerEvent : N.Event {

    /// The code of the incoming event
    public string id;

    /// Is this a start or stop event?
    public bool active;

    /// The player id
    public int playerId;

    /// Attempt to resolve the key id as the given type
    public bool Is<T>(T value, int playerId) where T : struct, IConvertible {
      if (playerId == this.playerId) {
        var match = N.Reflect.Enum.Resolve<T>(id);
        if (match) {
          return EqualityComparer<T>.Default.Equals(match.Unwrap(), value);
        }
      }
      return false;
    }
  }
}
