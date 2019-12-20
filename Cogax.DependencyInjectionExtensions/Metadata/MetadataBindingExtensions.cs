using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Cogax.DependencyInjectionExtensions.Metadata
{
    public static class MetadataBindingExtensions
    {
        public static IMetadataAttacher Bind<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            ServiceDescriptor originalDescriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);
            ServiceDescriptor implementationOnlyDescriptor = new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), lifetime);

            services.Add(originalDescriptor);
            services.Add(implementationOnlyDescriptor);

            return new ServiceDescriptorMetadataAttacher<TService>(services, implementationOnlyDescriptor);
        }

        public static object Resolve<TService>(this IServiceProvider serviceProvider, string key = null, object value = null)
        {
            if (key != null)
            {
                Type bindingType = typeof(ServiceDescriptorMetadata<>).MakeGenericType(typeof(TService));
                IEnumerable<ServiceDescriptorMetadata<TService>> bindingMetadataList = (IEnumerable<ServiceDescriptorMetadata<TService>>)serviceProvider.GetServices(bindingType);

                ServiceDescriptorMetadata<TService> bindingMetadata = bindingMetadataList?.FirstOrDefault(x => x.Key == key && x.Value == value);
                if (bindingMetadata != null)
                {
                    return serviceProvider.GetService(bindingMetadata.ServiceDescriptor.ImplementationType);
                }
            }

            return serviceProvider.GetService(typeof(TService));
        }
    }
}
