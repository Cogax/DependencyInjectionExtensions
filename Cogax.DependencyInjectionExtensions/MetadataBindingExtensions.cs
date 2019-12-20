using System;
using System.Collections.Generic;
using System.Linq;
using Cogax.DependencyInjectionExtensions.Bindings;
using Microsoft.Extensions.DependencyInjection;

namespace Cogax.DependencyInjectionExtensions
{
    public static class MetadataBindingExtensions
    {
        public static IMetadataAttacher AddTransient<TService, TImplementation>(this IServiceCollection services)
        {
            ServiceDescriptor realDescriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient);
            ServiceDescriptor implementationOnlyDescriptor = new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), ServiceLifetime.Transient);
            services.Add(implementationOnlyDescriptor);
            return new ServiceDescriptorMetadataAttacher<TService>(services, implementationOnlyDescriptor);
        }

        public static object GetServiceWithMetadata<TService>(this IServiceProvider serviceProvider, string key, object value)
        {
            Type bindingType = typeof(ServiceDescriptorMetadata<>).MakeGenericType(typeof(TService));
            IEnumerable<ServiceDescriptorMetadata<TService>> bindingMetadatas = (IEnumerable<ServiceDescriptorMetadata<TService>>)serviceProvider.GetServices(bindingType);

            ServiceDescriptorMetadata<TService> bindingMetadata = bindingMetadatas?.FirstOrDefault(x => x.Key == key && x.Value == value);
            if (bindingMetadata != null)
            {
                return serviceProvider.GetService(bindingMetadata.ServiceDescriptor.ImplementationType);
            }

            return serviceProvider.GetService(typeof(TService));
        }

        public interface IMetadataAttacher
        {
            IMetadataAttacher WithMetadata(string key, object value);
        }

        public class ServiceDescriptorMetadataAttacher<TService> : IMetadataAttacher
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
                ServiceCollection.Remove(ServiceDescriptor);
                ServiceCollection.Add(ServiceDescriptor);

                ServiceDescriptorMetadata<TService> descriptorMetadata = new ServiceDescriptorMetadata<TService>(ServiceDescriptor, key, value);
                ServiceCollection.AddSingleton(descriptorMetadata);

                return this;
            }
        }
    }
}
