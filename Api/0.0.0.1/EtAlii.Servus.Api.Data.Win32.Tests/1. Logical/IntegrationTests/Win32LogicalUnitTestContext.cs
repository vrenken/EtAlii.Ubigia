namespace EtAlii.Servus.Api.Diagnostics.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Helpers.Win32.Tests;
    using EtAlii.Servus.Api.Logical.Tests;

    public class Win32LogicalUnitTestContext : IDisposable
    {
        public string TestFile_2M_Image;
        public string TestFile_10M_Raw;
        public string TestFile_100M_Raw;

        public ILogicalTestContext LogicalTestContext { get; private set; }

        public Win32LogicalUnitTestContext()
        {
            var task = Task.Run(() =>
            {
                LogicalTestContext = new LogicalTestContextFactory().Create();

                // Getting Temp file names to use
                TestFile_2M_Image = Win32TestHelper.CreateTemporaryFileName();
                TestFile_10M_Raw = Win32TestHelper.CreateTemporaryFileName();
                TestFile_100M_Raw = Win32TestHelper.CreateTemporaryFileName();

                Win32TestHelper.SaveResourceTestImage(TestFile_2M_Image);
                Win32TestHelper.SaveTestFile(TestFile_10M_Raw, 10);
                Win32TestHelper.SaveTestFile(TestFile_100M_Raw, 100);
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(() =>
            {
                if (File.Exists(TestFile_2M_Image))
                {
                    File.Delete(TestFile_2M_Image);
                }

                if (File.Exists(TestFile_10M_Raw))
                {
                    File.Delete(TestFile_10M_Raw);
                }

                if (File.Exists(TestFile_100M_Raw))
                {
                    File.Delete(TestFile_100M_Raw);
                }
                LogicalTestContext = null;
            });
            task.Wait();
        }
    }
}