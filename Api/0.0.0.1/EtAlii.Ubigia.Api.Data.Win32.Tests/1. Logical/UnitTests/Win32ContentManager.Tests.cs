namespace EtAlii.Ubigia.Api.Logical.Win32.UnitTests
{
    using System;
    using EtAlii.Ubigia.Api.Helpers.Win32.Tests;
    using System.IO;

    
    public class Win32ContentManagerTests : IDisposable
    {
        private readonly string _testImageFileName;

        public Win32ContentManagerTests()
        {
            // Getting Temp file name to use
            _testImageFileName = Win32TestHelper.CreateTemporaryFileName();

            Win32TestHelper.SaveResourceTestImage(_testImageFileName);
        }

        public void Dispose()
        {
            if (File.Exists(_testImageFileName))
            {
                File.Delete(_testImageFileName);
            }
        }

        //    [Fact]
        //    public void Win32ContentManager_Stubbed_Create()
        //    {
        //        // Arrange.
        //        var entries = new StubIEntries();
        //        var connection = new StubIDataConnection();
        //        connection.EntriesGet = () => entries;

        //        // Act.
        //        var contentManager = new Win32ContentManager(connection);

        //        // Assert.
        //    }

        //    [Fact]
        //    public void Win32ContentManager_Stubbed_Upload_Non_Existing_File()
        //    {
        //        // Arrange.
        //        var entries = new StubIEntries();
        //        var content = new StubIContent();
        //        var connection = new StubIDataConnection();
        //        connection.EntriesGet = () => entries;
        //        connection.ContentGet = () => content;

        //        var contentManager = new Win32ContentManager(connection);
        //        var fileName = Guid.NewGuid().ToString();

        //        // Act.
        //        var act = new Action(() =>
        //        {
        //            contentManager.Upload(fileName, Identifier.Empty);
        //        });

        //        // Assert.
        //        Assert.Throws<ContentManagerException>(act);
        //    }

        //    [Fact]
        //    public void Win32ContentManager_Stubbed_Upload_Existing_File()
        //    {
        //        // Arrange.
        //        var entries = new StubIEntries();
        //        var content = new StubIContent();
        //        var connection = new StubIDataConnection();
        //        connection.EntriesGet = () => entries;
        //        connection.ContentGet = () => content;
        //        var contentManager = new Win32ContentManager(connection);

        //        // Act.
        //        var act = new Action(() =>
        //        {
        //            contentManager.Upload(_testImageFileName, Identifier.Empty);
        //        });

        //        // Assert.
        //        Assert.Throws<ContentManagerException>(act);
        //    }
    }
}
