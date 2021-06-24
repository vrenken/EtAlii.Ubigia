// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.IO;
    using System.Reflection;
    using Xunit;

    public static class Win32TestHelper
    {
        public static string CreateTemporaryFileName()
        {
            var fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            return fileName;
        }

        public static string CreateTemporaryFolderName()
        {
            var folderName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

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
            using var stream = assembly!.GetManifestResourceStream("EtAlii.Ubigia.Api.Functional.Tests.TestImage_01.jpg");
            var bytes = new byte[stream!.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            File.WriteAllBytes(fileName, bytes);
        }

        public static void SaveTestFolder(string folderName, int depth, int foldersPerFolder, int filesPerFolder, float fileMinSize, float fileMaxSize)
        {
            Directory.CreateDirectory(folderName);

            if (depth > 0)
            {
                for (var folder = 0; folder < foldersPerFolder; folder++)
                {
                    var subFolder = Path.Combine(folderName, Guid.NewGuid().ToString());
                    SaveTestFolder(subFolder, depth - 1, foldersPerFolder, filesPerFolder, fileMinSize, fileMaxSize);
                }
            }

            for (var file = 0; file < filesPerFolder; file++)
            {
                var fileName = $"{Guid.NewGuid()}.bin";
                fileName = Path.Combine(folderName, fileName);
                SaveTestFile(fileName, fileMinSize, fileMaxSize);
            }
        }

        public static void SaveTestFile(string fileName, int megaBytes)
        {
            const int bytesInMegaByte = 1024 * 1024;
            var data = new byte[bytesInMegaByte];
            var rnd = new Random();

            using var stream = File.OpenWrite(fileName);

            for (var megaByte = 0; megaByte < megaBytes; megaByte++)
            {
                rnd.NextBytes(data);
                stream.Write(data, 0, bytesInMegaByte);
            }
        }

        private static void SaveTestFile(string fileName, float megaBytesMin, float megaBytesMax)
        {
            const int bytesInKiloByte = 1024;
            var data = new byte[bytesInKiloByte];
            var rnd = new Random();
            var megaBytes = megaBytesMin + rnd.NextDouble() * (megaBytesMax - megaBytesMin);

            var kiloBytes = (int)(megaBytes / 1024f);

            using var stream = File.OpenWrite(fileName);

            for (var kiloByte = 0; kiloByte < kiloBytes; kiloByte++)
            {
                rnd.NextBytes(data);
                stream.Write(data, 0, bytesInKiloByte);
            }
        }

        public static void AssertFilesAreEqual(string expectedFile, string actualFile)
        {
            var expectedBytes = File.ReadAllBytes(expectedFile);
            var actualBytes = File.ReadAllBytes(actualFile);
            Assert.Equal(expectedBytes.Length, actualBytes.Length);
            for (var i = 0; i < expectedBytes.Length; i++)
            {
                Assert.Equal(expectedBytes[i], actualBytes[i]);
            }
        }


        //public static IContentManager CreateContentManager(ILogicalContext logicalContext)
        //[
        //    return new ContentManagerFactory().Create(logicalContext.Fabric)
        //]
        //public static IContentManager CreateContentManager(IFabricContext fabric)
        //[
        //    return new ContentManagerFactory().Create(fabric)
        //]
    }
}
