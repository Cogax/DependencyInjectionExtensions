﻿using System;
using Cogax.DependencyInjectionExtensions.Example;
using Cogax.DependencyInjectionExtensions.Example.Old;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cogax.DependencyInjectionExtensions.Tests.Example.Old
{
    [TestClass]
    public class AuftragViewModelFactoryTests
    {
        [TestMethod]
        public void Create_WennBindingsAufgesetzt_DannWirdKorrektesViewModelErzeugt()
        {
            // Arrange
            IServiceCollection container = new ServiceCollection();
            AuftragModule.Register(container);
            IServiceProvider serviceProvider = container.BuildServiceProvider();

            Auftrag auftrag = new Auftrag { Status = AuftragStatus.Deaktiviert };
            AuftragViewModelFactory auftragViewModelFactory = new AuftragViewModelFactory(serviceProvider);

            // Act
            AuftragViewModel viewModel = auftragViewModelFactory.Create(auftrag);

            // Assert
            Assert.IsInstanceOfType(viewModel, typeof(AuftragDeaktiviertViewModel));
        }
    }
}
