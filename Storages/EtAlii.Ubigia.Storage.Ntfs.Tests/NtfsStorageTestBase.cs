namespace EtAlii.Ubigia.Storage.Ntfs.Tests
{
    using System;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;


    public abstract class NtfsStorageTestBase : IDisposable
    {
        protected IStorage Storage { get; private set; }

        private readonly string _rootFolder = @"c:\temp\" + Guid.NewGuid() + @"\";

        public NtfsStorageTestBase()
        {
            Storage = CreateStorage();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }

        public void Dispose()
        {
            Storage = null;
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
            return factory.Create("EtAlii", "EtAlii.Ubigia.Storage.Ntfs.Tests");
        }

        private IProfiler CreateProfiler(IProfilerFactory factory)
        {
            return factory.Create("EtAlii", "EtAlii.Ubigia.Storage.Ntfs.Tests");
        }
    }
}
