// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.IO;

    //you have to label the class with this or it is never scanned for methods
    public abstract class FileSystemStorageTestBase : IDisposable
    {
        protected IStorage Storage { get; set; }

        protected readonly string RootFolder = @"c:\temp\" + Guid.NewGuid() + @"\";

        protected FileSystemStorageTestBase()
        {
            Directory.CreateDirectory(RootFolder);
            AppDomain.CurrentDomain.ProcessExit += (_,_) => DeleteTestData();
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

        ~FileSystemStorageTestBase()
        {
            Dispose(false);
        }
    }
}
