// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using EtAlii.Ubigia.Persistence.Azure;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public class AzureStorageExtensionTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void AzureStorageExtension_Create()
        {
            // Arrange.

            // Act.
            var extension = new AzureStorageExtension();

            // Assert.
            Assert.NotNull(extension);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void AzureStorageExtension_Initialize()
        {
            // Arrange.
            var extension = new AzureStorageExtension();
            var container = new Container();

            // Act.
            extension.Initialize(container);

            // Assert.
            Assert.IsType<DefaultContainerProvider>(container.GetInstance<IContainerProvider>());
        }
    }
}
