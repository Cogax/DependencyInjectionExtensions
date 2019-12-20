using System;
using System.Collections.Concurrent;

namespace Cogax.DependencyInjectionExtensions.Bindings
{
    public class BindingMetadata<TConstraint>
    {
        public TConstraint Constraint { get; set; }

        public BindingMetadata(TConstraint constraint)
        {
            Constraint = constraint;
        }

        public bool Matches(BindingMetadata<TConstraint> requestedBindingMetadata)
        {
            return Constraint.Equals(requestedBindingMetadata.Constraint);
        }
    }

    // https://github.com/ninject/Ninject/blob/master/src/Ninject/Planning/Bindings/BindingMetadata.cs
    public class NewBindingMetadata
    {
        public string Key { get; }
        public object Value { get; }

        public NewBindingMetadata(string key, object value)
        {
            if(string.IsNullOrWhiteSpace(key)) throw new ArgumentException("A binding metadata key is required!", nameof(key));

            Key = key;
            Value = value;
        }
    }

    public class NewBindingMetadata<TService> : NewBindingMetadata
    {
        public Type ImplementationType { get; }

        public NewBindingMetadata(Type implementationType, string key, object value) : base(key, value)
        {
            ImplementationType = implementationType;
        }
    }
}
