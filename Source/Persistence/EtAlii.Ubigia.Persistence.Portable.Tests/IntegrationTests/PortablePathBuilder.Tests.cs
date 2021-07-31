// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using PCLStorage;
    using Xunit;

    public class PortablePathBuilderTests: IAsyncLifetime
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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortablePathBuilder_GetDirectoryName_Simple()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var directoryName = _testContext.Storage.PathBuilder.GetDirectoryName(string.Join(PortablePath.DirectorySeparatorChar.ToString(), containerId.Paths));

            // Assert.
            var paths = containerId.Paths.Take(containerId.Paths.Length == 1 ? 1 : containerId.Paths.Length - 1);
            var expectedDirectoryName = string.Join(PortablePath.DirectorySeparatorChar.ToString(), paths);
            Assert.Equal(expectedDirectoryName, directoryName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortablePathBuilder_GetDirectoryName_Complex()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier("First");

            // Act.
            var directoryName = _testContext.Storage.PathBuilder.GetDirectoryName(string.Join(PortablePath.DirectorySeparatorChar.ToString(), containerId.Paths));

            // Assert.
            var expectedDirectoryName = Path.GetDirectoryName(string.Join(PortablePath.DirectorySeparatorChar.ToString(), containerId.Paths));
            Assert.Equal(expectedDirectoryName, directoryName);
        }
    }
}
