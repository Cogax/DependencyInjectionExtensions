using Microsoft.Extensions.DependencyInjection;

namespace Cogax.DependencyInjectionExtensions.Example.Old
{
    public class AuftragModule
    {
        public static void Register(IServiceCollection container)
        {
            container.AddTransient<AuftragDeaktiviertViewModel>();
            container.AddTransient<AuftragErfasstViewModel>();
        }
    }
}
