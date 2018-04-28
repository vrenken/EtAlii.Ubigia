namespace EtAlii.Ubigia.Api.Functional.NET47.Tests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Helpers.NET47.Tests;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.NET47;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    
    public class NET47ContentManagerTests : IClassFixture<NET47LogicalUnitTestContext>, IDisposable
    {

        private readonly NET47LogicalUnitTestContext _testContext;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return _testContext;
        //    }
        //    set
        //    {
        //        _testContext = value;
        //    }
        //}
        //private TestContext _testContext;

        public NET47ContentManagerTests(NET47LogicalUnitTestContext testContext)
        {
            _testContext = testContext;

            var task = Task.Run(async () =>
            {
                await _testContext.LogicalTestContext.Start();
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                await _testContext.LogicalTestContext.Stop();
            });
            task.Wait();
        }

        [Fact]
        public async Task NET47ContentManager_Create()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);

            // Act.
            var contentManager = logicalContext.Content;

            // Assert.
        }


        [Fact]
        public async Task NET47ContentManager_Upload_Non_Existing_File()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var fileName = Guid.NewGuid().ToString();
            var contentManager = logicalContext.Content;

            // Act.
            var act = new Func<Task>(async () => await contentManager.Upload(fileName, entry.Id));

            // Assert.
            await Assert.ThrowsAsync<ContentManagerException>(act);
        }


        [Fact]
        public async Task NET47ContentManager_Upload_2M_File()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;

            // Act.
            await contentManager.Upload(_testContext.TestFile2MImage, entry.Id);

            // Assert.
        }

        [Fact]
        public async Task NET47ContentManager_Upload_2M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;

            // Act.

            var startTicks = Environment.TickCount;
            await contentManager.Upload(_testContext.TestFile2MImage, entry.Id);
            var endTicks = Environment.TickCount;

            // Assert.
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.True(duration < 0.5d, $"10M upload took: {duration} seconds");
        }

        [Fact]
        public async Task NET47ContentManager_Upload_10M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;

            // Act.
            var startTicks = Environment.TickCount;
            await contentManager.Upload(_testContext.TestFile10MRaw, entry.Id);
            var endTicks = Environment.TickCount;

            // Assert.
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.True(duration < 1d, $"10M upload took: {duration} seconds");
        }

        [Fact]
        public async Task NET47ContentManager_Upload_100M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;

            // Act.
            var startTicks = Environment.TickCount;
            await contentManager.Upload(_testContext.TestFile100MRaw, entry.Id);
            var endTicks = Environment.TickCount;

            // Assert.
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.True(duration < 2d, $"100M upload took: {duration} seconds");
        }

        [Fact]
        public async Task NET47ContentManager_Download_2M_File()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;
            var retrievedFilePath = NET47TestHelper.CreateTemporaryFileName();
            await contentManager.Upload(_testContext.TestFile2MImage, entry.Id);

            // Act.
            await contentManager.Download(retrievedFilePath, entry.Id);

            //// Assert.
            Assert.True(File.Exists(retrievedFilePath));
            _testContext.FileComparer.AreEqual(_testContext.TestFile2MImage, retrievedFilePath);

            // Assure.
            if (File.Exists(retrievedFilePath))
            {
                File.Delete(retrievedFilePath);
            }
        }

        [Fact]
        public async Task NET47ContentManager_Download_10M_File()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;
            var retrievedFilePath = NET47TestHelper.CreateTemporaryFileName();
            await contentManager.Upload(_testContext.TestFile10MRaw, entry.Id);

            // Act.
            await contentManager.Download(retrievedFilePath, entry.Id);

            //// Assert.
            Assert.True(File.Exists(retrievedFilePath));
            _testContext.FileComparer.AreEqual(_testContext.TestFile10MRaw, retrievedFilePath);

            // Assure.
            if (File.Exists(retrievedFilePath))
            {
                File.Delete(retrievedFilePath);
            }
        }

        [Fact]
        public async Task NET47ContentManager_Download_2M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;
            var retrievedFilePath = NET47TestHelper.CreateTemporaryFileName();
            await contentManager.Upload(_testContext.TestFile2MImage, entry.Id);

            // Act.
            var startTicks = Environment.TickCount;
            await contentManager.Download(retrievedFilePath, entry.Id);
            var endTicks = Environment.TickCount;

            //// Assert.
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
        public async Task NET47ContentManager_Download_10M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;
            var retrievedFilePath = NET47TestHelper.CreateTemporaryFileName();
            await contentManager.Upload(_testContext.TestFile10MRaw, entry.Id);

            // Act.
            var startTicks = Environment.TickCount;
            await contentManager.Download(retrievedFilePath, entry.Id);
            var endTicks = Environment.TickCount;

            //// Assert.
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
        public async Task NET47ContentManager_Download_100M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;
            var retrievedFilePath = NET47TestHelper.CreateTemporaryFileName();
            await contentManager.Upload(_testContext.TestFile100MRaw, entry.Id);

            // Act.
            var startTicks = Environment.TickCount;
            await contentManager.Download(retrievedFilePath, entry.Id);
            var endTicks = Environment.TickCount;

            //// Assert.
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
