using UnityEngine;
using N;
using System.Collections.Generic;

namespace N.Package.Input {

  /// Implement this to make a handler that polls for input
  public interface RawInputHandler {

    /// Invoked every update to poll for input
    void Update(Events events);

    /// Iff this input is only valid if it checks every frame, Implement
    /// this method instead of Update.
    void UpdateFrame(Events events);
  }
}
