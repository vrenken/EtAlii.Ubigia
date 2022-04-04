// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;
    using Serilog;

    public class ContentFunctionalUnitTestContext : IDisposable
    {
        /// <summary>
        ///Gets or sets the client configuration root that should be used to configure any client-specific components.
        ///</summary>
        public IConfigurationRoot ClientConfiguration => LogicalTestContext.ClientConfiguration;

        /// <summary>
        ///Gets or sets the host configuration root that should be used to configure any host-specific components.
        ///</summary>
        public IConfigurationRoot HostConfiguration => LogicalTestContext.HostConfiguration;

        public string TestFile2MImage { get; }
        public string TestFile10MRaw { get; }
        public string TestFile100MRaw { get; }

        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }
        public ILogicalTestContext LogicalTestContext { get; private set; }

        private readonly ILogger _logger;
        public ContentFunctionalUnitTestContext()
        {
            _logger = Log.ForContext<ContentFunctionalUnitTestContext>();
            _logger.Information("Created {InstanceName}", nameof(ContentFunctionalUnitTestContext));

            FileComparer = new FileComparer();
            FolderComparer = new FolderComparer(FileComparer);

            LogicalTestContext = new LogicalTestContextFactory().Create();

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            // using (var _ = new SystemSafeExecutionScope(_uniqueId))
            // {
                // Getting Temp file names to use
                TestFile2MImage = ContentTestHelper.CreateTemporaryFileName();
                TestFile10MRaw = ContentTestHelper.CreateTemporaryFileName();
                TestFile100MRaw = ContentTestHelper.CreateTemporaryFileName();

                ContentTestHelper.SaveResourceTestImage(TestFile2MImage);
                ContentTestHelper.SaveTestFile(TestFile10MRaw, 10);
                ContentTestHelper.SaveTestFile(TestFile100MRaw, 100);
            // }
        }

        private void OnProcessExit(object sender, EventArgs e) => RemoveTestFiles();

        private void RemoveTestFiles()
        {
            _logger.Information("Removing test files");

            if (File.Exists(TestFile2MImage))
            {
                File.Delete(TestFile2MImage);
            }

            if (File.Exists(TestFile10MRaw))
            {
                File.Delete(TestFile10MRaw);
            }

            if (File.Exists(TestFile100MRaw))
            {
                File.Delete(TestFile100MRaw);
            }
        }

        public async Task<Root> GetRoot(LogicalOptions logicalOptions, string rootName)
        {
            using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);

            return await logicalContext.Roots.GetAll()
                .SingleOrDefaultAsync(r => r.Name == rootName)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> GetEntry(LogicalOptions logicalOptions, Identifier identifier, ExecutionScope scope)
        {
            using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);

            return await logicalContext.Nodes
                .SelectSingle(GraphPath.Create(identifier), scope)
                .ConfigureAwait(false);
        }

        public void Dispose()
        {
            RemoveTestFiles();
            AppDomain.CurrentDomain.ProcessExit -= OnProcessExit;

            LogicalTestContext = null;
            GC.SuppressFinalize(this);
        }
    }
}
