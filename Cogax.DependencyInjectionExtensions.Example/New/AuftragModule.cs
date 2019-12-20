using Cogax.DependencyInjectionExtensions.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Cogax.DependencyInjectionExtensions.Example.New
{
    public class AuftragModule
    {
        public static void Register(IServiceCollection container)
        {
            container.Bind<AuftragViewModel, AuftragDeaktiviertViewModel>()
                .WithMetadata(BindingKeys.AuftragsStatus, AuftragStatus.Deaktiviert);

            container.Bind<AuftragViewModel, AuftragErfasstViewModel>()
                .WithMetadata(BindingKeys.AuftragsStatus, AuftragStatus.Erfasst);
        }
    }

    public class BindingKeys
    {
        public const string AuftragsStatus = "AuftragsStatus";
    }
}
