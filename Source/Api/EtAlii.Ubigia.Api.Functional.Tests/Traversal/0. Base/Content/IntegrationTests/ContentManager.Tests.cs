﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !UBIGIA_RUN_TESTS_PER_ASSEMBLY
namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;
    using Xunit.Abstractions;

    public class ContentManagerTests : IClassFixture<ContentFunctionalUnitTestContext>, IAsyncLifetime
    {
        private readonly ContentFunctionalUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private int _duration;

        public ContentManagerTests(ContentFunctionalUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var initialize = Environment.TickCount;

            await _testContext.LogicalTestContext.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);

            _testOutputHelper.WriteLine($"Initialize: {TimeSpan.FromTicks(Environment.TickCount - initialize).TotalMilliseconds}ms");

            _duration = Environment.TickCount;

        }

        public async Task DisposeAsync()
        {
            _testOutputHelper.WriteLine($"Test: {TimeSpan.FromTicks(Environment.TickCount - _duration).TotalMilliseconds}ms");

            var cleanup = Environment.TickCount;

            await _testContext.LogicalTestContext.Stop().ConfigureAwait(false);

            _testOutputHelper.WriteLine($"Cleanup: {TimeSpan.FromTicks(Environment.TickCount - cleanup).TotalMilliseconds}ms");
        }

        [Fact]
        public async Task Win32ContentManager_Create()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);

            // Act.
            var contentManager = Factory.Create<IContentManager>(logicalOptions);

            // Assert.
            Assert.NotNull(entry);
            Assert.NotNull(contentManager);
        }


        [Fact]
        public async Task Win32ContentManager_Upload_Non_Existing_File()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);

            var fileName = Guid.NewGuid().ToString();
            var contentManager = Factory.Create<IContentManager>(logicalOptions);

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
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);
            var contentManager = Factory.Create<IContentManager>(logicalOptions);

            // Act.
            var success = await contentManager.Upload(_testContext.TestFile2MImage, entry.Id).ConfigureAwait(false);

            // Assert.
            Assert.True(success);
        }

        [Fact]
        public async Task Win32ContentManager_Upload_2M_File_Timed()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);
            var contentManager = Factory.Create<IContentManager>(logicalOptions);

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
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);
            var contentManager = Factory.Create<IContentManager>(logicalOptions);

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
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);
            var contentManager = Factory.Create<IContentManager>(logicalOptions);

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
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);
            var contentManager = Factory.Create<IContentManager>(logicalOptions);
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
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);
            var contentManager = Factory.Create<IContentManager>(logicalOptions);
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
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);
            var contentManager = Factory.Create<IContentManager>(logicalOptions);
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
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);
            var contentManager = Factory.Create<IContentManager>(logicalOptions);
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
            var logicalOptions = await _testContext.LogicalTestContext
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
            await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

            var root = await _testContext
                .GetRoot(logicalContext, "Hierarchy")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalContext, root.Identifier, scope)
                .ConfigureAwait(false);
            var contentManager = Factory.Create<IContentManager>(logicalOptions);
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
