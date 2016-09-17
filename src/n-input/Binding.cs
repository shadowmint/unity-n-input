using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input
{
  /// A bound input mapping suitable for use by a controller
  public class Binding<TAction>
  {
    /// The input stream to use
    private readonly Inputs _inputs;

    /// Set of bound items
    private readonly IList<IBinding<TAction>> _bindings = new List<IBinding<TAction>>();

    /// Create a new instance
    public Binding(Inputs inputs)
    {
      _inputs = inputs;
    }

    /// Add a standard binding
    public void Bind(IBinding<TAction> binding)
    {
      if (!_bindings.Contains(binding))
      {
        _bindings.Add(binding);
      }
    }

    /// Clear bindings
    public void Clear()
    {
      _bindings.Clear();
    }

    /// Update and yield events
    public IEnumerable<TAction> Actions()
    {
      foreach (var input in _inputs.Stream())
      {
        foreach (var binding in _bindings)
        {
          foreach (var action in binding.Actions(input))
          {
            if (action == null) continue;
            var asAction = action as IAction;
            if (asAction != null)
            {
              asAction.Configure(input);
            }
            yield return action;
          }
        }
      }
    }
  }
}