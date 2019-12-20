using System;
using System.Collections.Generic;
using System.Linq;
using Cogax.DependencyInjectionExtensions.Bindings;
using Microsoft.Extensions.DependencyInjection;

namespace Cogax.DependencyInjectionExtensions
{
    public static class MetadataBindingExtensions
    {
        public static void AddTransientWithMetadata<TService, TImplementation>(this IServiceCollection services, string key, object value)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddSingleton(new NewBindingMetadata<TService>(typeof(TImplementation), key, value));
            services.AddTransient<TImplementation>();
        }

        public static object GetServiceWithMetadata<TService>(this IServiceProvider serviceProvider, string key, object value)
        {
            Type bindingType = typeof(NewBindingMetadata<>).MakeGenericType(typeof(TService));
            IEnumerable<NewBindingMetadata<TService>> bindingMetadatas = (IEnumerable<NewBindingMetadata<TService>>)serviceProvider.GetServices(bindingType);

            NewBindingMetadata<TService> bindingMetadata = bindingMetadatas?.FirstOrDefault(x => x.Key == key && x.Value == value);
            if (bindingMetadata != null)
            {
                return serviceProvider.GetService(bindingMetadata.ImplementationType);
            }

            return serviceProvider.GetService(typeof(TService));
        }
    }
}
