namespace EtAlii.Ubigia.Api.Logical.Win32.IntegrationTests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Helpers.Win32.Tests;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using Ubigia.Tests;

    public class Win32LogicalUnitTestContext : IDisposable
    {
        public string TestFile2MImage;
        public string TestFile10MRaw;
        public string TestFile100MRaw;

        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }
        public ILogicalTestContext LogicalTestContext { get; private set; }

        public Win32LogicalUnitTestContext()
        {
            FileComparer = new FileComparer();
            FolderComparer = new FolderComparer(FileComparer);

            LogicalTestContext = new LogicalTestContextFactory().Create();

            // Getting Temp file names to use
            TestFile2MImage = Win32TestHelper.CreateTemporaryFileName();
            TestFile10MRaw = Win32TestHelper.CreateTemporaryFileName();
            TestFile100MRaw = Win32TestHelper.CreateTemporaryFileName();

            Win32TestHelper.SaveResourceTestImage(TestFile2MImage);
            Win32TestHelper.SaveTestFile(TestFile10MRaw, 10);
            Win32TestHelper.SaveTestFile(TestFile100MRaw, 100);
        }

        public void Dispose()
        {
            var task = Task.Run(() =>
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
            });
            task.Wait();
        }
    }
}