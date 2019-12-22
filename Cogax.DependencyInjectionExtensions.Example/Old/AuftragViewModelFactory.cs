using System;
using Cogax.DependencyInjectionExtensions.Example.New;
using Microsoft.Extensions.DependencyInjection;

namespace Cogax.DependencyInjectionExtensions.Example.Old
{
public class AuftragViewModelFactory
{
    private readonly IServiceProvider _serviceProvider;

    public AuftragViewModelFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public AuftragViewModel Create(Auftrag auftrag)
    {
        AuftragViewModel viewModel = null;

        if (auftrag.Status.Equals(AuftragStatus.Deaktiviert))
        {
            viewModel = _serviceProvider.GetService<AuftragDeaktiviertViewModel>();
        }
        else if (auftrag.Status.Equals(AuftragStatus.Erfasst))
        {
            viewModel = _serviceProvider.GetService<AuftragErfasstViewModel>();
        }

        if (viewModel == null)
        {
            throw new Exception($"Kein Binding für {nameof(BindingKeys.AuftragsStatus)} '{auftrag.Status}' vorhanden!");
        }

        return viewModel;
    }
}
}
