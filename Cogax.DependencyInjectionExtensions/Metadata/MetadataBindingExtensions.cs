using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Cogax.DependencyInjectionExtensions.Metadata
{
    public static class MetadataBindingExtensions
    {
        /// <summary>
        /// Bind an implementation of a service with additional metadata configuration support.
        /// </summary>
        /// <typeparam name="TService">The generic type of the service</typeparam>
        /// <typeparam name="TImplementation">The generic typo of the service implementation</typeparam>
        /// <param name="services">The service container</param>
        /// <param name="lifetime">The lifetime of the binding, defaults to transient</param>
        /// <returns>An object, which can configure metadata for this binding</returns>
        public static IMetadataAttacher Bind<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
            where TService : class
            where TImplementation : class, TService
        {
            ServiceDescriptor originalDescriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);
            ServiceDescriptor implementationOnlyDescriptor = new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), lifetime);

            services.Add(originalDescriptor);
            services.Add(implementationOnlyDescriptor);

            return new ServiceDescriptorMetadataAttacher<TService>(services, implementationOnlyDescriptor);
        }

        /// <summary>
        /// Resolve an instance of a service definition with repect to metadata settings.
        /// </summary>
        /// <typeparam name="TService">The generic type of the service definition to be resolved</typeparam>
        /// <param name="serviceProvider">The service provider</param>
        /// <param name="key">The optional metadata key</param>
        /// <param name="value">The optional metadata value</param>
        /// <returns>An instance of the resolved type</returns>
        public static object Resolve<TService>(this IServiceProvider serviceProvider, string key = null, object value = null)
        {
            return serviceProvider.Resolve<TService>(x => x.Matches(key, value));
        }

        /// <summary>
        /// Resolve an instance of a service definition with repect to metadata settings.
        /// </summary>
        /// <typeparam name="TService">The generic type of the service definition to be resolved</typeparam>
        /// <param name="serviceProvider">The service provider</param>
        /// <param name="metadataMatcher">The function to determine if metadata matches</param>
        /// <returns>An instance of the resolved type</returns>
        public static object Resolve<TService>(this IServiceProvider serviceProvider, Func<ServiceDescriptorMetadata<TService>, bool> metadataMatcher)
        {
            if (metadataMatcher != null)
            {
                Type bindingMetadataType = typeof(ServiceDescriptorMetadata<>).MakeGenericType(typeof(TService));
                IEnumerable<ServiceDescriptorMetadata<TService>> bindingMetadataList =
                    (IEnumerable<ServiceDescriptorMetadata<TService>>)serviceProvider.GetServices(bindingMetadataType);

                ServiceDescriptorMetadata<TService> bindingMetadata = bindingMetadataList?.FirstOrDefault(x => metadataMatcher(x));
                if (bindingMetadata != null)
                {
                    return serviceProvider.GetService(bindingMetadata.ServiceDescriptor.ImplementationType);
                }
            }

            return serviceProvider.GetService(typeof(TService));
        }

    }
}
