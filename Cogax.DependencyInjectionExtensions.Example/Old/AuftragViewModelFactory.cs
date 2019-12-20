using System;
using Cogax.DependencyInjectionExtensions.Metadata;

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
            AuftragViewModel viewModel = _serviceProvider.Resolve<AuftragViewModel>(BindingKeys.AuftragsStatus, auftrag.Status);

            if (viewModel == null)
            {
                throw new InvalidCastException($"Kein Binding für {nameof(BindingKeys.AuftragsStatus)} '{auftrag.Status}' vorhanden!");
            }

            return viewModel;
        }
    }
}
