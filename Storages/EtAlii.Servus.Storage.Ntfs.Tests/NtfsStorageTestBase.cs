namespace EtAlii.Servus.Storage.Ntfs.Tests
{
    using System;
    using EtAlii.Servus.Storage;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using Xunit;

    
    public abstract class NtfsStorageTestBase : IDisposable
    {
        protected IStorage Storage { get { return _storage; } }
        private IStorage _storage;

        private readonly string _rootFolder = @"c:\temp\" + Guid.NewGuid() + @"\";

        public NtfsStorageTestBase()
        {
            _storage = CreateStorage();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }

        public void Dispose()
        {
            _storage = null;
        }

        private IStorage CreateStorage()
        {
            var diagnostics = new DiagnosticsConfiguration
            {
                EnableProfiling = false,
                EnableLogging = false,
                EnableDebugging = false,
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
            return factory.Create("EtAlii", "EtAlii.Servus.Storage.Ntfs.Tests");
        }

        private IProfiler CreateProfiler(IProfilerFactory factory)
        {
            return factory.Create("EtAlii", "EtAlii.Servus.Storage.Ntfs.Tests");
        }
    }
}
