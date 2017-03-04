namespace EtAlii.Ubigia.Storage.Portable.Tests
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using Xunit;
    using PCLStorage;

    
    public abstract class PortableStorageTestBase : IDisposable
    {
        protected IStorage Storage => _storage;
        private IStorage _storage;

        protected IFolder StorageFolder => _storageFolder;
        private IFolder _storageFolder;

        private string _rootFolder;
        private static int _testIndex;


        public PortableStorageTestBase()
        {
            _rootFolder = @"c:\temp\" + _testIndex;
            _testIndex += 1;
            _testIndex = _testIndex > 999 ? 0 : _testIndex;

            if (Directory.Exists(_rootFolder))
            {
                Directory.Delete(_rootFolder, true);
            }
            Directory.CreateDirectory(_rootFolder);

            _storageFolder = new FileSystemFolder(_rootFolder, false);
            //_storageFolder = FileSystem.Current.LocalStorage;

            _storage = CreateStorage();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }

        public void Dispose()
        {
            _storageFolder = null;
            _storage = null;

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
                .UsePortableStorage(_storageFolder);

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
