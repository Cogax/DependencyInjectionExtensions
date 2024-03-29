﻿using Microsoft.Extensions.DependencyInjection;

namespace Cogax.DependencyInjectionExtensions.Metadata
{
    internal class ServiceDescriptorMetadataAttacher<TService> : IMetadataAttacher
    {
        public IServiceCollection ServiceCollection { get; }
        public ServiceDescriptor ServiceDescriptor { get; }

        public ServiceDescriptorMetadataAttacher(IServiceCollection serviceCollection, ServiceDescriptor serviceDescriptor)
        {
            ServiceCollection = serviceCollection;
            ServiceDescriptor = serviceDescriptor;
        }

        public IMetadataAttacher WithMetadata(string key, object value)
        {
            ServiceDescriptorMetadata<TService> descriptorMetadata = new ServiceDescriptorMetadata<TService>(ServiceDescriptor, key, value);
            ServiceCollection.AddSingleton(descriptorMetadata);

            return this;
        }
    }
}