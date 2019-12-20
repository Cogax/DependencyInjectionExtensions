using System;
using System.Collections.Generic;
using System.Linq;
using Cogax.DependencyInjectionExtensions.Bindings;
using Microsoft.Extensions.DependencyInjection;

namespace Cogax.DependencyInjectionExtensions
{
    public static class ConstraintBindingExtensions
    {
        public static void AddTransientWithConstraint<TService, TImplementation, TConstraint>(this IServiceCollection services, TConstraint constraint)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddSingleton(new ConstraintBinding<TService, TConstraint>(typeof(TImplementation), constraint));
            services.AddTransient<TImplementation>();
        }

        public static object GetServiceRespectConstraint<TService, TConstraint>(this IServiceProvider serviceProvider, TConstraint constraint)
        {
            Type bindingType = typeof(ConstraintBinding<,>).MakeGenericType(typeof(TService), typeof(TConstraint));
            IEnumerable<ConstraintBinding<TService, TConstraint>> bindings = (IEnumerable<ConstraintBinding<TService, TConstraint>>)serviceProvider.GetServices(bindingType);
            ConstraintBinding<TService, TConstraint> binding = bindings?.FirstOrDefault(x => x.ServiceType == typeof(TService) && x.BindingMetadata.Matches(new BindingMetadata<TConstraint>(constraint)));
            if (binding != null)
            {
                return serviceProvider.GetService(binding.ImplementationType);
            }

            return serviceProvider.GetService(typeof(TService));
        }
    }
}
