namespace EtAlii.Ubigia.Api.Functional.NET47.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.NET47;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using Xunit;


    public class NET47HierarchicalContentManagerTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private readonly string _testFolderSimple;
        private readonly LogicalUnitTestContext _testContext;

        public NET47HierarchicalContentManagerTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
            // Getting Temp folder names to use
            _testFolderSimple = NET47TestHelper.CreateTemporaryFolderName();
        }

        public async Task InitializeAsync()
        {
            await NET47TestHelper.SaveTestFolder(_testFolderSimple, 3, 3, 3, 0.2f, 1.2f);
        }

        public Task DisposeAsync()
        {
            if (Directory.Exists(_testFolderSimple))
            {
                Directory.Delete(_testFolderSimple, true);
            }
            return Task.CompletedTask;
        }

        [Fact]
        public async Task NET47HierarchicalContentManager_Create()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var contentManager = logicalContext.Content;

            // Act.
            var hierarchicalContentManager = new HierarchicalContentManager(logicalContext, contentManager);

            // Assert.
            Assert.NotNull(hierarchicalContentManager);
        }

        [Fact(Skip="Not working yet")]
        public async Task NET47HierarchicalContentManager_Upload_Non_Existing_Folder_Hierarchy()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var folderName = Guid.NewGuid().ToString();
            var contentManager = logicalContext.Content;
            var hierarchicalContentManager = new HierarchicalContentManager(logicalContext, contentManager);

            // Act.
            var act = new Action(() =>
            {
                hierarchicalContentManager.Upload(folderName, entry.Id);
            });

            // Assert.
            Assert.Throws<ContentManagerException>(act);
        }


        [Fact]
        public async Task NET47HierarchicalContentManager_Upload_Folder_Hierarchy()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;
            var hierarchicalContentManager = new HierarchicalContentManager(logicalContext, contentManager);

            // Act.
            hierarchicalContentManager.Upload(_testFolderSimple, entry.Id);

            // Assert.
        }


        [Fact(Skip = "Not working yet")]
        public async Task NET47HierarchicalContentManager_Download_Folder_Hierarchy()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;
            var hierarchicalContentManager = new HierarchicalContentManager(logicalContext, contentManager);
            var retrievedFolderPath = NET47TestHelper.CreateTemporaryFolderName();
            hierarchicalContentManager.Upload(_testFolderSimple, entry.Id);

            // Act.
            hierarchicalContentManager.Download(retrievedFolderPath, entry.Id);

            //// Assert.
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
