namespace EtAlii.Ubigia.Api.Functional.NET47.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using Ubigia.Tests;
    using Xunit;

    public class NET47LogicalUnitTestContext : IAsyncLifetime
    {
        public readonly string TestFile2MImage;
        public readonly string TestFile10MRaw;
        public readonly string TestFile100MRaw;

        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }
        public ILogicalTestContext LogicalTestContext { get; private set; }

        public NET47LogicalUnitTestContext()
        {
            FileComparer = new FileComparer();
            FolderComparer = new FolderComparer(FileComparer);

            LogicalTestContext = new LogicalTestContextFactory().Create();

            // Getting Temp file names to use
            TestFile2MImage = NET47TestHelper.CreateTemporaryFileName();
            TestFile10MRaw = NET47TestHelper.CreateTemporaryFileName();
            TestFile100MRaw = NET47TestHelper.CreateTemporaryFileName();
        }

        public async Task InitializeAsync()
        {
            await NET47TestHelper.SaveResourceTestImage(TestFile2MImage);
            await NET47TestHelper.SaveTestFile(TestFile10MRaw, 10);
            await NET47TestHelper.SaveTestFile(TestFile100MRaw, 100);
        }

        public Task DisposeAsync()
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
            return Task.CompletedTask;
        }
    }
}