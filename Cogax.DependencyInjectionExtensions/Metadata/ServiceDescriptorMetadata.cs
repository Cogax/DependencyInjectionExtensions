using System;
using Microsoft.Extensions.DependencyInjection;

namespace Cogax.DependencyInjectionExtensions.Metadata
{
    public class ServiceDescriptorMetadata<TService>
    {
        public ServiceDescriptor ServiceDescriptor { get; }
        public string Key { get; }
        public object Value { get; }

        public ServiceDescriptorMetadata(ServiceDescriptor serviceDescriptor, string key, object value)
        {
            if (serviceDescriptor == null) throw new ArgumentException("A service descriptor is required!", nameof(serviceDescriptor));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("A binding metadata key is required!", nameof(key));

            ServiceDescriptor = serviceDescriptor;
            Key = key;
            Value = value;
        }

        public bool Matches(string key, object value)
        {
            return Key.Equals(key) && Value.Equals(value);
        }
    }
}