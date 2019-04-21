﻿namespace EtAlii.Ubigia.Api.Functional.NET47.Tests
{
    using System.IO;
    using System.Threading.Tasks;
    using Xunit;

    public class NET47ContentManager2Tests : IAsyncLifetime
    {
        private readonly string _testImageFileName;

        public NET47ContentManager2Tests()
        {
            // Getting Temp file name to use
            _testImageFileName = NET47TestHelper.CreateTemporaryFileName();
        }
        public async Task InitializeAsync()
        {
            await NET47TestHelper.SaveResourceTestImage(_testImageFileName);
        }

        public Task DisposeAsync()
        {
            if (File.Exists(_testImageFileName))
            {
                File.Delete(_testImageFileName);
            }
            return Task.CompletedTask;
        }

        //    [Fact]
        //    public void NET47ContentManager_Stubbed_Create()
        //    [
        //        // Arrange.
        //        var entries = new StubIEntries()
        //        var connection = new StubIDataConnection()
        //        connection.EntriesGet = () => entries

        //        // Act.
        //        var contentManager = new NET47ContentManager(connection)

        //        // Assert.
        //    ]
        //    [Fact]
        //    public void NET47ContentManager_Stubbed_Upload_Non_Existing_File()
        //    [
        //        // Arrange.
        //        var entries = new StubIEntries()
        //        var content = new StubIContent()
        //        var connection = new StubIDataConnection()
        //        connection.EntriesGet = () => entries
        //        connection.ContentGet = () => content

        //        var contentManager = new NET47ContentManager(connection)
        //        var fileName = Guid.NewGuid().ToString()

        //        // Act.
        //        var act = new Action(() =>
        //        [
        //            contentManager.Upload(fileName, Identifier.Empty)
        //        ])

        //        // Assert.
        //        Assert.Throws<ContentManagerException>(act)
        //    ]
        //    [Fact]
        //    public void NET47ContentManager_Stubbed_Upload_Existing_File()
        //    [
        //        // Arrange.
        //        var entries = new StubIEntries()
        //        var content = new StubIContent()
        //        var connection = new StubIDataConnection()
        //        connection.EntriesGet = () => entries
        //        connection.ContentGet = () => content
        //        var contentManager = new NET47ContentManager(connection)

        //        // Act.
        //        var act = new Action(() =>
        //        [
        //            contentManager.Upload(_testImageFileName, Identifier.Empty)
        //        ])

        //        // Assert.
        //        Assert.Throws<ContentManagerException>(act)
        //    ]
    }
}
