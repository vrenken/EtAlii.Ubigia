namespace EtAlii.Ubigia.Persistence.Ntfs.Tests
{
    using System;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public abstract class NtfsStorageTestBase : IDisposable
    {
        protected TestContentFactory TestContentFactory { get; }
        protected TestContentDefinitionFactory TestContentDefinitionFactory { get; }
        protected TestPropertiesFactory TestPropertiesFactory { get; }

        protected IStorage Storage { get; private set; }

        private readonly string _rootFolder = @"c:\temp\" + Guid.NewGuid() + @"\";

        protected NtfsStorageTestBase()
        {
            TestContentFactory = new TestContentFactory();
            TestContentDefinitionFactory = new TestContentDefinitionFactory();
            TestPropertiesFactory = new TestPropertiesFactory();

            Storage = CreateStorage();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                Storage = null;
            }
        }

        ~NtfsStorageTestBase()
        {
            Dispose(false);
        }

        private IStorage CreateStorage()
        {
            var diagnostics = new DiagnosticsConfiguration
            {
                EnableProfiling = true,
                EnableLogging = true,
                EnableDebugging = true,
                CreateLogFactory = () => new DisabledLogFactory(),
                CreateLogger = CreateLogger,
                CreateProfilerFactory = () => new DisabledProfilerFactory(),
                CreateProfiler = CreateProfiler,
            };

            var configuration = new StorageConfiguration()
                .Use(TestAssembly.StorageName)
                .UseNtfsStorage(_rootFolder)
                .Use(diagnostics);

            return new StorageFactory().Create(configuration);
        }

        private ILogger CreateLogger(ILogFactory factory)
        {
            return factory.Create("EtAlii", "EtAlii.Ubigia.Persistence.Ntfs.Tests");
        }

        private IProfiler CreateProfiler(IProfilerFactory factory)
        {
            return factory.Create("EtAlii", "EtAlii.Ubigia.Persistence.Ntfs.Tests");
        }
    }
}
