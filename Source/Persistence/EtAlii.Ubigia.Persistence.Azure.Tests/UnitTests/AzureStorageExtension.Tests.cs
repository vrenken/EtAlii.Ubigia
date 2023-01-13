// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using EtAlii.Ubigia.Persistence.Azure;
using EtAlii.xTechnology.MicroContainer;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class AzureStorageExtensionTests
{
    [Fact]
    public void AzureStorageExtension_Create()
    {
        // Arrange.

        // Act.
        var extension = new AzureStorageExtension();

        // Assert.
        Assert.NotNull(extension);
    }

    [Fact]
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
