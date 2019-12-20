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

        [TestMethod]
        public void WhenBindTwoServicesWithMetadata_ThenCorrectServicesAreResolved()
        {
            // Arrange
            var container = new ServiceCollection();
            container.AddTransient<IMyService, MyServiceA>().WithMetadata("typ", "A");
            container.AddTransient<IMyService, MyServiceB>().WithMetadata("typ", "B");
            IServiceProvider serviceProvider = container.BuildServiceProvider();

            // Act
            var serviceA = serviceProvider.GetServiceWithMetadata<IMyService>("typ", "A");
            var serviceB = serviceProvider.GetServiceWithMetadata<IMyService>("typ", "B");

            // Assert
            Assert.IsInstanceOfType(serviceA, typeof(MyServiceA));
            Assert.IsNotInstanceOfType(serviceA, typeof(MyServiceB));

            Assert.IsInstanceOfType(serviceB, typeof(MyServiceB));
            Assert.IsNotInstanceOfType(serviceB, typeof(MyServiceA));
        }


        //[TestMethod]
        //public void WhenBindTwoServicesWithConditionalConstraint_ThenCorrectServicesAreResolved()
        //{
        //    // Arrange
        //    var container = new ServiceCollection();
        //    container.AddTransientWithConstraint<IMyService, MyServiceA, Func<Foo, bool>>((foo) => foo.Bar == "A");
        //    container.AddTransientWithConstraint<IMyService, MyServiceA, Func<Foo, bool>>((foo) => foo.Bar == "B");

        //    IServiceProvider serviceProvider = container.BuildServiceProvider();

        //    // Act
        //    var serviceA = serviceProvider.GetServiceRespectConstraint<IMyService, Foo>();
        //    var serviceB = serviceProvider.GetServiceRespectConstraint<IMyService, Foo>();

        //    // Assert
        //    Assert.IsInstanceOfType(serviceA, typeof(MyServiceA));
        //    Assert.IsNotInstanceOfType(serviceA, typeof(MyServiceB));

        //    Assert.IsInstanceOfType(serviceB, typeof(MyServiceB));
        //    Assert.IsNotInstanceOfType(serviceB, typeof(MyServiceA));
        //}

        private class Foo
        {
            public string Bar { get; set; }
        }

        private interface IMyService { }
        private class MyServiceA : IMyService { }
        private class MyServiceB : IMyService { }
        private enum MyEnum { A, B }
    }
}
