using System;
using System.Collections.Generic;
using System.Text;

namespace Cogax.DependencyInjectionExtensions.Bindings
{
    public class ConstraintBinding<TService, TConstraint>
    {
        public Type ServiceType { get; set; }
        public Type ImplementationType { get; set; }
        public BindingMetadata<TConstraint> BindingMetadata { get; set; }

        public ConstraintBinding(Type implementationType, TConstraint bindingConstraint)
        {
            ServiceType = typeof(TService);
            ImplementationType = implementationType;
            BindingMetadata = new BindingMetadata<TConstraint>(bindingConstraint);
        }
    }
}
