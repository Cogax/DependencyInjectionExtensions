using System;
using Cogax.DependencyInjectionExtensions.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cogax.DependencyInjectionExtensions.Tests.Metadata
{
    [TestClass]
    public class MetadataBindingExtensionsTests
    {
        [TestMethod]
        public void BindTwoServiceImplementations_WhenResolveBothWithExistingMetadata_ThenServicesAreResolvedCorrectly()
        {
            // Arrange
            var container = new ServiceCollection();
            container.Bind<IMyService, MyServiceA>().WithMetadata(BindingKeys.EventType, "A");
            container.Bind<IMyService, MyServiceB>().WithMetadata(BindingKeys.EventType, "B");
            IServiceProvider serviceProvider = container.BuildServiceProvider();

            // Act
            var serviceA = serviceProvider.Resolve<IMyService>(BindingKeys.EventType, "A");
            var serviceB = serviceProvider.Resolve<IMyService>(BindingKeys.EventType, "B");

            // Assert
            Assert.IsInstanceOfType(serviceA, typeof(MyServiceA));
            Assert.IsNotInstanceOfType(serviceA, typeof(MyServiceB));

            Assert.IsInstanceOfType(serviceB, typeof(MyServiceB));
            Assert.IsNotInstanceOfType(serviceB, typeof(MyServiceA));
        }

        [TestMethod]
        public void BindTwoServiceImplementations_WhenResolveBothWithMetadataMatcher_ThenServicesAreResolvedCorrectly()
        {
            // Arrange
            var container = new ServiceCollection();
            container.Bind<IMyService, MyServiceA>().WithMetadata(BindingKeys.EventType, "A");
            container.Bind<IMyService, MyServiceB>().WithMetadata(BindingKeys.EventType, "B");
            IServiceProvider serviceProvider = container.BuildServiceProvider();

            // Act
            var serviceA = serviceProvider.Resolve<IMyService>(x => (string) x.Value == "A");
            var serviceB = serviceProvider.Resolve<IMyService>(x => (string) x.Value == "B");

            // Assert
            Assert.IsInstanceOfType(serviceA, typeof(MyServiceA));
            Assert.IsNotInstanceOfType(serviceA, typeof(MyServiceB));

            Assert.IsInstanceOfType(serviceB, typeof(MyServiceB));
            Assert.IsNotInstanceOfType(serviceB, typeof(MyServiceA));
        }

        [TestMethod]
        public void BindTwoServiceImplementations_WhenResolveWithNotExistingMetadata_ThenLastBoundServiceIsResolved()
        {
            // Arrange
            var container = new ServiceCollection();
            container.Bind<IMyService, MyServiceA>().WithMetadata(BindingKeys.EventType, "A");
            container.Bind<IMyService, MyServiceB>().WithMetadata(BindingKeys.EventType, "B");
            IServiceProvider serviceProvider = container.BuildServiceProvider();

            // Act
            var service = serviceProvider.Resolve<IMyService>(BindingKeys.EventType, "C");

            // Assert
            Assert.IsInstanceOfType(service, typeof(MyServiceB));
            Assert.IsNotInstanceOfType(service, typeof(MyServiceA));
        }

        [TestMethod]
        public void BindTwoServiceImplementations_WhenResolveWithoutMetadata_ThenLastBoundServiceIsResolved()
        {
            // Arrange
            var container = new ServiceCollection();
            container.Bind<IMyService, MyServiceA>().WithMetadata(BindingKeys.EventType, "A");
            container.Bind<IMyService, MyServiceB>().WithMetadata(BindingKeys.EventType, "B");
            IServiceProvider serviceProvider = container.BuildServiceProvider();

            // Act
            var service = serviceProvider.Resolve<IMyService>();

            // Assert
            Assert.IsInstanceOfType(service, typeof(MyServiceB));
            Assert.IsNotInstanceOfType(service, typeof(MyServiceA));
        }

        private class BindingKeys
        {
            public const string EventType = "EventType";
        }

        private interface IMyService { }
        private class MyServiceA : IMyService { }
        private class MyServiceB : IMyService { }
    }
}
