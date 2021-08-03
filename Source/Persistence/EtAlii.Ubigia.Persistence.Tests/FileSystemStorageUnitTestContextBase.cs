// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    //you have to label the class with this or it is never scanned for methods
    public abstract class FileSystemStorageUnitTestContextBase : StorageUnitTestContextBase
    {
        public IStorage Storage { get; protected set; }

        public readonly string RootFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()) + Path.DirectorySeparatorChar;

        public override async Task InitializeAsync()
        {
            await base
                .InitializeAsync()
                .ConfigureAwait(false);

            Directory.CreateDirectory(RootFolder);
            AppDomain.CurrentDomain.ProcessExit += DeleteTestData;
        }


        public override async Task DisposeAsync()
        {

            await base
                .DisposeAsync()
                .ConfigureAwait(false);

            // Cleanup
            Storage = null;

            AppDomain.CurrentDomain.ProcessExit -= DeleteTestData;
            DeleteTestData(null, null);
        }

        private void DeleteTestData(object sender, EventArgs eventArgs)
        {
            // Let's cleanup.
            if(Directory.Exists(RootFolder))
            {
                Directory.Delete(RootFolder, true);
            }
        }
    }
}
