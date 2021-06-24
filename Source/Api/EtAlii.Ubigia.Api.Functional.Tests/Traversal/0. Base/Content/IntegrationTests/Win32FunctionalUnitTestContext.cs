// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Hosting;
    using Serilog;

    public class Win32FunctionalUnitTestContext : IDisposable
    {
        private readonly Guid _uniqueId = Guid.Parse("5F763915-44A5-496F-B478-BFA42F60E406");
        public string TestFile2MImage { get; }
        public string TestFile10MRaw { get; }
        public string TestFile100MRaw { get; }

        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }
        public ILogicalTestContext LogicalTestContext { get; private set; }

        private readonly ILogger _log;
        public Win32FunctionalUnitTestContext()
        {
            _log = Log.ForContext<Win32FunctionalUnitTestContext>();
            _log.Information("Created {InstanceName}", nameof(Win32FunctionalUnitTestContext));

            FileComparer = new FileComparer();
            FolderComparer = new FolderComparer(FileComparer);

            LogicalTestContext = new LogicalTestContextFactory().Create();

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            using (var _ = new SystemSafeExecutionScope(_uniqueId))
            {
                // Getting Temp file names to use
                TestFile2MImage = Win32TestHelper.CreateTemporaryFileName();
                TestFile10MRaw = Win32TestHelper.CreateTemporaryFileName();
                TestFile100MRaw = Win32TestHelper.CreateTemporaryFileName();

                Win32TestHelper.SaveResourceTestImage(TestFile2MImage);
                Win32TestHelper.SaveTestFile(TestFile10MRaw, 10);
                Win32TestHelper.SaveTestFile(TestFile100MRaw, 100);
            }
        }

        private void OnProcessExit(object sender, EventArgs e) => RemoveTestFiles();

        private void RemoveTestFiles()
        {
            _log.Information("Removing test files.");

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
        public void Dispose()
        {
            RemoveTestFiles();
            AppDomain.CurrentDomain.ProcessExit -= OnProcessExit;

            LogicalTestContext = null;
            GC.SuppressFinalize(this);
        }
    }
}
