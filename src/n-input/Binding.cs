using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Input
{
    /// A bound input mapping suitable for use by a controller
    public class Binding<TAction>
    {
        /// The input stream to use
        private Inputs inputs;

        /// Set of bound items
        private IList<IBinding<TAction>> bindings = new List<IBinding<TAction>>();

        /// Create a new instance
        public Binding(Inputs inputs)
        {
            this.inputs = inputs;
        }

        /// Add a standard binding
        public void Bind(IBinding<TAction> binding)
        {
            if (!bindings.Contains(binding))
            {
                bindings.Add(binding);
            }
        }

        /// Clear bindings
        public void Clear()
        {
            bindings.Clear();
        }

        /// Update and yeild events
        public IEnumerable<TAction> Actions()
        {
            foreach (var input in inputs.Stream())
            {
                foreach (var binding in bindings)
                {
                    foreach (var action in binding.Actions(input))
                    {
                        if (action != null)
                        {
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
}
