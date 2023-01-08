namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
    using System;
    using Xunit;

    public class SystemConnectionOptionsTests
    {
        [Fact]
        public void SystemConnectionOptions_Create()
        {
            // Arrange.

            // Act.
            var options = new SystemConnectionOptions(null);

            // Assert.
            Assert.NotNull(options);
        }

        [Fact]
        public void SystemConnectionOptions_Use_Factory_Extension()
        {
            // Arrange.
            var options = new SystemConnectionOptions(null);

            // Act.
            options = options.Use(() => null);

            // Assert.
            Assert.NotNull(options);
            Assert.NotNull(options.FactoryExtension);
        }

        [Fact]
        public void SystemConnectionOptions_Use_Functional_Context_Null()
        {
            // Arrange.
            var options = new SystemConnectionOptions(null);

            // Act.
            var act = new Action(() => options.Use((IFunctionalContext)null));

            // Assert.
            Assert.Throws<ArgumentNullException>(act);
        }


        [Fact]
        public void SystemConnectionOptions_Use_Functional_Context()
        {
            // Arrange.
            var options = new SystemConnectionOptions(null);
            var name = "Test";
            var storageAddress = new Uri("https://nowhere.com");
            var serviceDetails = new []
            {
                new ServiceDetails("Test", new Uri("https://nowhere.com/management"), new Uri("https://nowhere.com/data"), new Uri("https://nowhere.com/storage"))
            };
            var functionalContextOptions = new FunctionalContextOptions(null).Use(name, storageAddress, serviceDetails);
            var functionalContext = new FunctionalContext(functionalContextOptions, null, null, null, null, null, null, null, null, null, null, null, null);

            // Act.
            options = options.Use(functionalContext);

            // Assert.
            Assert.NotNull(options);
            Assert.NotNull(options.TransportProvider);
            Assert.Same(serviceDetails, options.ServiceDetails);
        }

        [Fact]
        public void SystemConnectionOptions_Use_Functional_Context_Twice()
        {
            // Arrange.
            var options = new SystemConnectionOptions(null);
            var name = "Test";
            var storageAddress = new Uri("https://nowhere.com");
            var serviceDetails = new []
            {
                new ServiceDetails("Test", new Uri("https://nowhere.com/management"), new Uri("https://nowhere.com/data"), new Uri("https://nowhere.com/storage"))
            };
            var functionalContextOptions = new FunctionalContextOptions(null).Use(name, storageAddress, serviceDetails);
            var functionalContext = new FunctionalContext(functionalContextOptions, null, null, null, null, null, null, null, null, null, null, null, null);
            options = options.Use(functionalContext);

            // Act.
            var act = new Action(() => options.Use(functionalContext));

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
