namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Networking;

    public class Win32LogicalUnitTestContext : IDisposable
    {
        private readonly Guid _uniqueId = Guid.Parse("5F763915-44A5-496F-B478-BFA42F60E406"); 
        public string TestFile2MImage { get; }
        public string TestFile10MRaw { get; }
        public string TestFile100MRaw { get; }

        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }
        public ILogicalTestContext LogicalTestContext { get; private set; }

        public Win32LogicalUnitTestContext()
        {
            FileComparer = new FileComparer();
            FolderComparer = new FolderComparer(FileComparer);

            LogicalTestContext = new LogicalTestContextFactory().Create();

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

        public void Dispose()
        {
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
            LogicalTestContext = null;
        }
    }
}