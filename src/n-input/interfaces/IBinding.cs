using System.Collections.Generic;

namespace N.Package.Input
{
  /// A prebaked input binding from player preferences
  public interface IBinding<TAction>
  {
    /// Generate the next set of input actions
    IEnumerable<TAction> Actions(IInput input);
  }
}