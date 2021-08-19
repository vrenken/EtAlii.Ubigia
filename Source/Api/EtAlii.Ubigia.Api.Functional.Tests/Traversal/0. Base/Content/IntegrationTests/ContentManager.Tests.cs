// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !UBIGIA_RUN_TESTS_PER_ASSEMBLY
namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;
    using UnitTestSettings = EtAlii.Ubigia.Api.Functional.Tests.UnitTestSettings;

    public class ContentManagerTests : IClassFixture<ContentFunctionalUnitTestContext>, IAsyncLifetime
    {
        private readonly ContentFunctionalUnitTestContext _testContext;

        public ContentManagerTests(ContentFunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            await _testContext.LogicalTestContext.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await _testContext.LogicalTestContext.Stop().ConfigureAwait(false);
        }

        [Fact]
        public async Task Win32ContentManager_Create()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);

            // Act.
            var contentManager = logicalContext.Content;

            // Assert.
            Assert.NotNull(entry);
            Assert.NotNull(contentManager);
        }


        [Fact]
        public async Task Win32ContentManager_Upload_Non_Existing_File()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var fileName = Guid.NewGuid().ToString();
            var contentManager = logicalContext.Content;

            // Act.
            var act = new Func<Task>(async () => await contentManager.Upload(fileName, entry.Id).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<ContentManagerException>(act).ConfigureAwait(false);
        }


        [Fact]
        public async Task Win32ContentManager_Upload_2M_File()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var contentManager = logicalContext.Content;

            // Act.
            await contentManager.Upload(_testContext.TestFile2MImage, entry.Id).ConfigureAwait(false);

            // Assert.
        }

        [Fact]
        public async Task Win32ContentManager_Upload_2M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var contentManager = logicalContext.Content;

            // Act.

            var startTicks = Environment.TickCount;
            await contentManager.Upload(_testContext.TestFile2MImage, entry.Id).ConfigureAwait(false);
            var endTicks = Environment.TickCount;

            // Assert.
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.True(duration < 0.5d, $"10M upload took: {duration} seconds");
        }

        [Fact]
        public async Task Win32ContentManager_Upload_10M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var contentManager = logicalContext.Content;

            // Act.
            var startTicks = Environment.TickCount;
            await contentManager.Upload(_testContext.TestFile10MRaw, entry.Id).ConfigureAwait(false);
            var endTicks = Environment.TickCount;

            // Assert.
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.True(duration < 1d, $"10M upload took: {duration} seconds");
        }

        [Fact]
        public async Task Win32ContentManager_Upload_100M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var contentManager = logicalContext.Content;

            // Act.
            var startTicks = Environment.TickCount;
            await contentManager.Upload(_testContext.TestFile100MRaw, entry.Id).ConfigureAwait(false);
            var endTicks = Environment.TickCount;

            // Assert.
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.True(duration < 2d, $"100M upload took: {duration} seconds");
        }

        [Fact]
        public async Task Win32ContentManager_Download_2M_File()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var contentManager = logicalContext.Content;
            var retrievedFilePath = ContentTestHelper.CreateTemporaryFileName();
            await contentManager.Upload(_testContext.TestFile2MImage, entry.Id).ConfigureAwait(false);

            // Act.
            await contentManager.Download(retrievedFilePath, entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.True(File.Exists(retrievedFilePath));
            _testContext.FileComparer.AreEqual(_testContext.TestFile2MImage, retrievedFilePath);

            // Assure.
            if (File.Exists(retrievedFilePath))
            {
                File.Delete(retrievedFilePath);
            }
        }

        [Fact]
        public async Task Win32ContentManager_Download_10M_File()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var contentManager = logicalContext.Content;
            var retrievedFilePath = ContentTestHelper.CreateTemporaryFileName();
            await contentManager.Upload(_testContext.TestFile10MRaw, entry.Id).ConfigureAwait(false);

            // Act.
            await contentManager.Download(retrievedFilePath, entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.True(File.Exists(retrievedFilePath));
            _testContext.FileComparer.AreEqual(_testContext.TestFile10MRaw, retrievedFilePath);

            // Assure.
            if (File.Exists(retrievedFilePath))
            {
                File.Delete(retrievedFilePath);
            }
        }

        [Fact]
        public async Task Win32ContentManager_Download_2M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var contentManager = logicalContext.Content;
            var retrievedFilePath = ContentTestHelper.CreateTemporaryFileName();
            await contentManager.Upload(_testContext.TestFile2MImage, entry.Id).ConfigureAwait(false);

            // Act.
            var startTicks = Environment.TickCount;
            await contentManager.Download(retrievedFilePath, entry.Id).ConfigureAwait(false);
            var endTicks = Environment.TickCount;

            // Assert.
            Assert.True(File.Exists(retrievedFilePath));
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds;
            Assert.True(duration < 500, $"2M download took: {duration}ms");

            // Assure.
            if (File.Exists(retrievedFilePath))
            {
                File.Delete(retrievedFilePath);
            }
        }

        [Fact]
        public async Task Win32ContentManager_Download_10M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var contentManager = logicalContext.Content;
            var retrievedFilePath = ContentTestHelper.CreateTemporaryFileName();
            await contentManager.Upload(_testContext.TestFile10MRaw, entry.Id).ConfigureAwait(false);

            // Act.
            var startTicks = Environment.TickCount;
            await contentManager.Download(retrievedFilePath, entry.Id).ConfigureAwait(false);
            var endTicks = Environment.TickCount;

            // Assert.
            Assert.True(File.Exists(retrievedFilePath));
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds;
            Assert.True(duration < 1000, $"10M download took: {duration}ms");

            // Assure.
            if (File.Exists(retrievedFilePath))
            {
                File.Delete(retrievedFilePath);
            }
        }

        [Fact]
        public async Task Win32ContentManager_Download_100M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.LogicalTestContext
                .CreateLogicalContextWithConnection(true)
                .ConfigureAwait(false);
            var root = await logicalContext.Roots
                .Get("Hierarchy")
                .ConfigureAwait(false);
            var entry = await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(root.Identifier), scope)
                .ConfigureAwait(false);
            var contentManager = logicalContext.Content;
            var retrievedFilePath = ContentTestHelper.CreateTemporaryFileName();
            await contentManager.Upload(_testContext.TestFile100MRaw, entry.Id).ConfigureAwait(false);

            // Act.
            var startTicks = Environment.TickCount;
            await contentManager.Download(retrievedFilePath, entry.Id).ConfigureAwait(false);
            var endTicks = Environment.TickCount;

            // Assert.
            Assert.True(File.Exists(retrievedFilePath));
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds;
            Assert.True(duration < 2000, $"100M download took: {duration}ms");

            // Assure.
            if (File.Exists(retrievedFilePath))
            {
                File.Delete(retrievedFilePath);
            }
        }
    }
}
#endif
