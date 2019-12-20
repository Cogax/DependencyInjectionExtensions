using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cogax.DependencyInjectionExtensions.Tests
{
    [TestClass]
    public class ConstraintBindingExtensionsTests
    {
        [TestMethod]
        public void WhenBindTwoServicesWithEnumConstraint_ThenCorrectServicesAreResolved()
        {
            // Arrange
            var container = new ServiceCollection();
            container.AddTransientWithConstraint<IMyService, MyServiceA, MyEnum>(MyEnum.A);
            container.AddTransientWithConstraint<IMyService, MyServiceB, MyEnum>(MyEnum.B);
            
            IServiceProvider serviceProvider = container.BuildServiceProvider();

            // Act
            var serviceA = serviceProvider.GetServiceRespectConstraint<IMyService, MyEnum>(MyEnum.A);
            var serviceB = serviceProvider.GetServiceRespectConstraint<IMyService, MyEnum>(MyEnum.B);

            // Assert
            Assert.IsInstanceOfType(serviceA, typeof(MyServiceA));
            Assert.IsNotInstanceOfType(serviceA, typeof(MyServiceB));

            Assert.IsInstanceOfType(serviceB, typeof(MyServiceB));
            Assert.IsNotInstanceOfType(serviceB, typeof(MyServiceA));
        }

        private interface IMyService { }
        private class MyServiceA : IMyService { }
        private class MyServiceB : IMyService { }
        private enum MyEnum { A, B }
    }
}
