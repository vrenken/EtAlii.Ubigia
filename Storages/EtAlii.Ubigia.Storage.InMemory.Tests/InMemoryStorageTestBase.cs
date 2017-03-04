namespace EtAlii.Ubigia.Storage.InMemory.Tests
{
    using System;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using Xunit;

    
    public abstract class InMemoryStorageTestBase : IDisposable
    {
        protected InMemoryStorage Storage => _storage;
        private InMemoryStorage _storage;

        public InMemoryStorageTestBase()
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

        private InMemoryStorage CreateStorage()
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
                .UseInMemoryStorage()
                .Use(diagnostics);

            return (InMemoryStorage)new StorageFactory().Create(configuration);
        }

        private ILogger CreateLogger(ILogFactory factory)
        {
            return factory.Create("EtAlii", "EtAlii.Ubigia.Storage.InMemory.Tests");
        }

        private IProfiler CreateProfiler(IProfilerFactory factory)
        {
            return factory.Create("EtAlii", "EtAlii.Ubigia.Storage.InMemory.Tests");
        }

    }
}
