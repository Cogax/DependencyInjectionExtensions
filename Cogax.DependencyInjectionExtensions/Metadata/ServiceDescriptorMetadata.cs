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
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("A binding metadata key is required!", nameof(key));

            Key = key;
            Value = value;
            ServiceDescriptor = serviceDescriptor;
        }
    }
}