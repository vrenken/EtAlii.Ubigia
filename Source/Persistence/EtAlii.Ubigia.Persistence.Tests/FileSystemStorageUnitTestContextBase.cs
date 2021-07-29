// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.IO;

    //you have to label the class with this or it is never scanned for methods
    public abstract class FileSystemStorageUnitTestContextBase : StorageUnitTestContextBase
    {
        public IStorage Storage { get; set; }

        public readonly string RootFolder = @"c:\temp\" + Guid.NewGuid() + @"\";

        protected FileSystemStorageUnitTestContextBase()
        {
            Directory.CreateDirectory(RootFolder);
            AppDomain.CurrentDomain.ProcessExit += (_,_) => DeleteTestData();
        }

        protected override void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                Storage = null;
            }

            DeleteTestData();
        }

        private void DeleteTestData()
        {
            // Let's cleanup.
            if(Directory.Exists(RootFolder))
            {
                Directory.Delete(RootFolder, true);
            }
        }

        ~FileSystemStorageUnitTestContextBase()
        {
            Dispose(false);
        }
    }
}
