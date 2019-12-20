using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

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
    public class ServiceDescriptorMetadata
    {
        public string Key { get; }
        public object Value { get; }

        public ServiceDescriptorMetadata(string key, object value)
        {
            if(string.IsNullOrWhiteSpace(key)) throw new ArgumentException("A binding metadata key is required!", nameof(key));

            Key = key;
            Value = value;
        }
    }

    public class ServiceDescriptorMetadata<TService> : ServiceDescriptorMetadata
    {
        public ServiceDescriptor ServiceDescriptor { get; }

        public ServiceDescriptorMetadata(ServiceDescriptor serviceDescriptor, string key, object value)
            : base(key, value)
        {
            ServiceDescriptor = serviceDescriptor;
        }
    }
}
