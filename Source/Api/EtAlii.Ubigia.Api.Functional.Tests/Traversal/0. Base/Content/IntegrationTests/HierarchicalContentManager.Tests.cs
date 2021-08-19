// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class HierarchicalContentManagerTests : IClassFixture<FunctionalUnitTestContext>, IDisposable
    {
        private readonly string _testFolderSimple;
        private readonly FunctionalUnitTestContext _testContext;

        public HierarchicalContentManagerTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;

            // Getting Temp folder names to use
            _testFolderSimple = ContentTestHelper.CreateTemporaryFolderName();
            ContentTestHelper.SaveTestFolder(_testFolderSimple, 3, 3, 3, 0.2f, 1.2f);
        }

        public void Dispose()
        {
            if (Directory.Exists(_testFolderSimple))
            {
                Directory.Delete(_testFolderSimple, true);
            }
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void Win32HierarchicalContentManager_Create()
        {
            // Arrange.

            // Act.
            var hierarchicalContentManager = new HierarchicalContentManager();

            // Assert.
            Assert.NotNull(hierarchicalContentManager);
        }

        [Fact(Skip="Not working yet")]
        public async Task Win32HierarchicalContentManager_Upload_Non_Existing_Folder_Hierarchy()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var folderName = Guid.NewGuid().ToString();
            var hierarchicalContentManager = new HierarchicalContentManager();

            // Act.
            var act = new Action(() =>
            {
                hierarchicalContentManager.Upload(folderName, entry.Id);
            });

            // Assert.
            Assert.Throws<ContentManagerException>(act);
        }


        [Fact]
        public async Task Win32HierarchicalContentManager_Upload_Folder_Hierarchy()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var hierarchicalContentManager = new HierarchicalContentManager();

            // Act.
            hierarchicalContentManager.Upload(_testFolderSimple, entry.Id);

            // Assert.
        }


        [Fact(Skip = "Not working yet")]
        public async Task Win32HierarchicalContentManager_Download_Folder_Hierarchy()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var hierarchicalContentManager = new HierarchicalContentManager();
            var retrievedFolderPath = ContentTestHelper.CreateTemporaryFolderName();
            hierarchicalContentManager.Upload(_testFolderSimple, entry.Id);

            // Act.
            hierarchicalContentManager.Download(retrievedFolderPath, entry.Id);

            // Assert.
            Assert.True(File.Exists(retrievedFolderPath));
            _testContext.FileComparer.AreEqual(_testFolderSimple, retrievedFolderPath);

            // Assure.
            if (Directory.Exists(retrievedFolderPath))
            {
                Directory.Delete(retrievedFolderPath);
            }
        }
    }
}
