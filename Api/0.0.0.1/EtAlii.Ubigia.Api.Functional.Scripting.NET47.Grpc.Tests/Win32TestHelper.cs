namespace EtAlii.Ubigia.Api.Functional.NET47.Tests
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public static class NET47TestHelper
    {
        public static string CreateTemporaryFileName()
        {
            var fileName = Path.GetTempFileName();

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            return fileName;
        }

        public static string CreateTemporaryFolderName()
        {
            var folderName = Path.GetTempFileName();

            if (File.Exists(folderName))
            {
                File.Delete(folderName);
            }

            return folderName;
        }

        public static async Task SaveResourceTestImage(string fileName)
        {
            // Get the current executing assembly (in this case it's the test dll)
            var assembly = Assembly.GetAssembly(typeof(NET47TestHelper));
            // Get the stream (embedded resource) - be sure to wrap in a using block
            using (Stream stream = assembly.GetManifestResourceStream("EtAlii.Ubigia.Api.Functional.NET47.Tests.TestImage_01.jpg"))
            {
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                await File.WriteAllBytesAsync(fileName, bytes);
            }
        }

        public static async Task SaveTestFolder(string folderName, int depth, int foldersPerFolder, int filesPerFolder, float fileMinSize, float fileMaxSize)
        {
            Directory.CreateDirectory(folderName);

            if (depth > 0)
            {
                for (int folder = 0; folder < foldersPerFolder; folder++)
                {
                    var subFolder = Path.Combine(folderName, Guid.NewGuid().ToString());
                    await SaveTestFolder(subFolder, depth - 1, foldersPerFolder, filesPerFolder, fileMinSize, fileMaxSize);
                }
            }

            for (int file = 0; file < filesPerFolder; file++)
            {
                var fileName = $"{Guid.NewGuid()}.bin";
                fileName = Path.Combine(folderName, fileName);
                await SaveTestFile(fileName, fileMinSize, fileMaxSize);
            }
        }

        public static async Task SaveTestFile(string fileName, int megaBytes)
        {
            const int bytesInMegaByte = 1024 * 1024;
            var data = new byte[bytesInMegaByte];
            var rnd = new Random();

            using (var stream = File.OpenWrite(fileName))
            {
                for (int megaByte = 0; megaByte < megaBytes; megaByte++)
                {
                    rnd.NextBytes(data);
                    await stream.WriteAsync(data, 0, bytesInMegaByte);
                }
            }
        }

        private static async Task SaveTestFile(string fileName, float megaBytesMin, float megaBytesMax)
        {
            const int bytesInKiloByte = 1024;
            var data = new byte[bytesInKiloByte];
            var rnd = new Random();
            var megaBytes = megaBytesMin + rnd.NextDouble() * (megaBytesMax - megaBytesMin);

            int kiloBytes = (int)(megaBytes / 1024f);
            using (var stream = File.OpenWrite(fileName))
            {
                for (int kiloByte = 0; kiloByte < kiloBytes; kiloByte++)
                {
                    rnd.NextBytes(data);
                    await stream.WriteAsync(data, 0, bytesInKiloByte);
                }
            }
        }

        public static async Task AssertFilesAreEqual(string expectedFile, string actualFile)
        {
            var expectedBytes = await File.ReadAllBytesAsync(expectedFile);
            var actualBytes = await File.ReadAllBytesAsync(actualFile);
            Assert.Equal(expectedBytes.Length, actualBytes.Length);
            for (int i = 0; i < expectedBytes.Length; i++)
            {
                Assert.Equal(expectedBytes[i], actualBytes[i]);
            }
        }


        //public static IContentManager CreateContentManager(ILogicalContext logicalContext)
        //{
        //    return new ContentManagerFactory().Create(logicalContext.Fabric)
        //}

        //public static IContentManager CreateContentManager(IFabricContext fabric)
        //{
        //    return new ContentManagerFactory().Create(fabric)
        //}
    }
}