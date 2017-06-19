namespace EtAlii.Ubigia.Api.Logical.Win32.IntegrationTests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Win32;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using Helpers.Win32.Tests;
    using Ubigia.Tests;
    using Xunit;


    public class Win32HierarchicalContentManagerTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private readonly string _testFolderSimple;
        private readonly LogicalUnitTestContext _testContext;

        public Win32HierarchicalContentManagerTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;

            // Getting Temp folder names to use
            _testFolderSimple = Win32TestHelper.CreateTemporaryFolderName();
            Win32TestHelper.SaveTestFolder(_testFolderSimple, 3, 3, 3, 0.2f, 1.2f);
        }

        public void Dispose()
        {
            if (Directory.Exists(_testFolderSimple))
            {
                Directory.Delete(_testFolderSimple, true);
            }
        }

        [Fact]
        public async Task Win32HierarchicalContentManager_Create()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var contentManager = logicalContext.Content;

            // Act.
            var hierarchicalContentManager = new HierarchicalContentManager(logicalContext, contentManager);

            // Assert.
        }

        [Fact(Skip="Not working yet")]
        public async Task Win32HierarchicalContentManager_Upload_Non_Existing_Folder_Hierarchy()
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
            ExceptionAssert.Throws<ContentManagerException>(act);
        }


        [Fact]
        public async Task Win32HierarchicalContentManager_Upload_Folder_Hierarchy()
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
        public async Task Win32HierarchicalContentManager_Download_Folder_Hierarchy()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var root = await logicalContext.Roots.Get("Hierarchy");
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), scope);
            var contentManager = logicalContext.Content;
            var hierarchicalContentManager = new HierarchicalContentManager(logicalContext, contentManager);
            var retrievedFolderPath = Win32TestHelper.CreateTemporaryFolderName();
            hierarchicalContentManager.Upload(_testFolderSimple, entry.Id);

            // Act.
            hierarchicalContentManager.Download(retrievedFolderPath, entry.Id);

            //// Assert.
            Assert.True(File.Exists(retrievedFolderPath));
            AssertData.FilesAreEqual(_testFolderSimple, retrievedFolderPath);

            // Assure.
            if (Directory.Exists(retrievedFolderPath))
            {
                Directory.Delete(retrievedFolderPath);
            }
        }
    }
}
