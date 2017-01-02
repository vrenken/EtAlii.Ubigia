namespace EtAlii.Ubigia.Api.Helpers.Win32.Tests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Fabric;
    using System;
    using System.IO;
    using System.Reflection;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;
    using Path = System.IO.Path;

    public static class Win32TestHelper
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

        public static void SaveResourceTestImage(string fileName)
        {
            // Get the current executing assembly (in this case it's the test dll)
            var assembly = Assembly.GetAssembly(typeof(Win32TestHelper));
            // Get the stream (embedded resource) - be sure to wrap in a using block
            using (Stream stream = assembly.GetManifestResourceStream("EtAlii.Ubigia.Api.Functional.Win32.Tests.TestImage_01.jpg"))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                File.WriteAllBytes(fileName, bytes);
            }
        }

        public static void SaveTestFolder(string folderName, int depth, int foldersPerFolder, int filesPerFolder, float fileMinSize, float fileMaxSize)
        {
            Directory.CreateDirectory(folderName);

            if (depth > 0)
            {
                for (int folder = 0; folder < foldersPerFolder; folder++)
                {
                    var subFolder = Path.Combine(folderName, Guid.NewGuid().ToString());
                    SaveTestFolder(subFolder, depth - 1, foldersPerFolder, filesPerFolder, fileMinSize, fileMaxSize);
                }
            }

            for (int file = 0; file < filesPerFolder; file++)
            {
                var fileName = String.Format("{0}.bin",Guid.NewGuid().ToString());
                fileName = Path.Combine(folderName, fileName);
                SaveTestFile(fileName, fileMinSize, fileMaxSize);
            }
        }

        public static void SaveTestFile(string fileName, int megaBytes)
        {
            const int bytesInMegaByte = 1024 * 1024;
            var data = new byte[bytesInMegaByte];
            var rnd = new Random();

            using (var stream = File.OpenWrite(fileName))
            {
                for (int megaByte = 0; megaByte < megaBytes; megaByte++)
                {
                    rnd.NextBytes(data);
                    stream.Write(data, 0, bytesInMegaByte);
                }
            }
        }

        public static void SaveTestFile(string fileName, float megaBytesMin, float megaBytesMax)
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
                    stream.Write(data, 0, bytesInKiloByte);
                }
            }
        }

        public static void AssertFilesAreEqual(string expectedFile, string actualFile)
        {
            var expectedBytes = File.ReadAllBytes(expectedFile);
            var actualBytes = File.ReadAllBytes(actualFile);
            Assert.Equal(expectedBytes.Length, actualBytes.Length);
            for (int i = 0; i < expectedBytes.Length; i++)
            {
                Assert.Equal(expectedBytes[i], actualBytes[i]);
            }
        }


        //public static IContentManager CreateContentManager(ILogicalContext logicalContext)
        //{
        //    return new ContentManagerFactory().Create(logicalContext.Fabric);
        //}

        //public static IContentManager CreateContentManager(IFabricContext fabric)
        //{
        //    return new ContentManagerFactory().Create(fabric);
        //}
    }
}