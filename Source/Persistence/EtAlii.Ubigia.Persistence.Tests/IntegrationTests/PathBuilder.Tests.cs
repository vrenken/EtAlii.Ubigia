// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using System.IO;
using System.Threading.Tasks;
using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public class PathBuilderTests : IAsyncLifetime
{
    private StorageUnitTestContext _testContext;

    public async Task InitializeAsync()
    {
        _testContext = new StorageUnitTestContext();
        await _testContext
            .InitializeAsync()
            .ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        await _testContext
            .DisposeAsync()
            .ConfigureAwait(false);
        _testContext = null;
    }

    [Fact]
    public void PathBuilder_GetFileNameWithoutExtension()
    {
        // Arrange.
        var fileName = "File";
        var fullFileName = $"{fileName}.Extension";

        // Act.
        var fileNameWithoutExtension = _testContext.Storage.PathBuilder.GetFileNameWithoutExtension(fullFileName);

        // Assert.
        Assert.Equal(fileName, fileNameWithoutExtension);
    }

    [Fact]
    public void PathBuilder_GetFolder()
    {
        // Arrange.
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

        // Act.
        var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);

        // Assert.
        var expectedFolder = Path.Combine(_testContext.Storage.PathBuilder.BaseFolder, Path.Combine(containerId.Paths));
        Assert.Equal(expectedFolder, folder);
    }

    [Fact]
    public void PathBuilder_GetDirectoryName()
    {
        // Arrange.
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

        // Act.
        var directoryName = _testContext.Storage.PathBuilder.GetDirectoryName(Path.Combine(containerId.Paths));

        // Assert.
        var expectedDirectoryName = _testContext.GetExpectedDirectoryName(containerId);
        Assert.Equal(expectedDirectoryName, directoryName);
    }
}
