namespace EtAlii.Ubigia.Storage.Portable.Tests
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using PCLStorage;

    
    public abstract class PortableStorageTestBase : IDisposable
    {
        protected IStorage Storage { get; private set; }

        protected IFolder StorageFolder { get; private set; }

        private readonly string _rootFolder;
        private static int _testIndex;


        protected PortableStorageTestBase()
        {
            _rootFolder = @"c:\temp\" + _testIndex;
            _testIndex += 1;
            _testIndex = _testIndex > 999 ? 0 : _testIndex;

            if (Directory.Exists(_rootFolder))
            {
                Directory.Delete(_rootFolder, true);
            }
            Directory.CreateDirectory(_rootFolder);

            StorageFolder = new FileSystemFolder(_rootFolder, false);
            //_storageFolder = FileSystem.Current.LocalStorage;

            Storage = CreateStorage();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }

        public void Dispose()
        {
            StorageFolder = null;
            Storage = null;

            if (Directory.Exists(_rootFolder))
            {
                Directory.Delete(_rootFolder, true);
            }
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
                .Use(diagnostics)
                .UsePortableStorage(StorageFolder);

            return new StorageFactory().Create(configuration);
        }

        private ILogger CreateLogger(ILogFactory factory)
        {
            return factory.Create("EtAlii", "EtAlii.Ubigia.Storage.Portable.Tests");
        }

        private IProfiler CreateProfiler(IProfilerFactory factory)
        {
            return factory.Create("EtAlii", "EtAlii.Ubigia.Storage.Portable.Tests");
        }
    }
}
