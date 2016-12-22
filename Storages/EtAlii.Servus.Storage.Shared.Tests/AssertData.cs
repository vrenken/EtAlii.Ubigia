namespace EtAlii.Servus.Storage.Tests
{
    using EtAlii.Servus.Api;
    using Xunit;
    using System;
    using System.IO;
    using System.Linq;

    public static class AssertData
    {
        public static void AreEqual(byte[] expected, byte[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        
        public static void AreNotEqual(byte[] first, byte[] second)
        {
            bool areEqual = true;
            
            try
            {
                areEqual &= first != second || (first == null && second == null);
                if (areEqual)
                {
                    areEqual &= first.Length == second.Length;
                }
                if (areEqual)
                {
                    for (int i = 0; i < first.Length; i++)
                    {
                        areEqual &= first[i] == second[i];
                    }
                }
            }
            catch
            {
                areEqual = false;
            }

            Assert.False(areEqual);
        }

        public static void AreEqual(IReadOnlyContentDefinition expected, IReadOnlyContentDefinition actual, bool checkSummary)
        {
            Assert.Equal(expected.Checksum, actual.Checksum);
            Assert.Equal(expected.Size, actual.Size);
            var expectedPartCount = expected.Parts.Count();
            var actualPartCount = actual.Parts.Count();
            Assert.Equal(expectedPartCount, actualPartCount);

            for (int i = 0; i < expectedPartCount; i++)
            {
                var expectedPart = expected.Parts.ElementAt(i);
                var actualPart = actual.Parts.ElementAt(i);

                AssertData.AreEqual(expectedPart, actualPart);
            }

            if (checkSummary)
            {
                AssertData.AreEqual(expected.Summary, actual.Summary);
            }
        }

        public static void AreEqual(BlobSummary expected, BlobSummary actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.IsComplete, actual.IsComplete);
            Assert.Equal(expected.TotalParts, actual.TotalParts);

            //Assert.Equal(expected.AvailableParts, actual.TotalParts);
        }

        public static void AreEqual(IReadOnlyContentDefinitionPart expected, IReadOnlyContentDefinitionPart actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Checksum, actual.Checksum);
            Assert.Equal(expected.Size, actual.Size);
        }

        public static void AreEqual(IPropertyDictionary expected, IPropertyDictionary actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
            foreach (var kvp in expected)
            {
                Assert.True(actual.ContainsKey(kvp.Key));
                Assert.Equal(kvp.Value, actual[kvp.Key]);
            }
        }

        public static void AreEqual(IReadOnlyContent expected, IReadOnlyContent actual, bool checkSummary)
        {
            Assert.Equal(expected.TotalParts, actual.TotalParts);

            if (checkSummary)
            {
                AreEqual(expected.Summary, actual.Summary);
            }
        }

        public static void AreEqual(IReadOnlyContentPart expected, IReadOnlyContentPart actual)
        {
            AreEqual(expected.Data, actual.Data);
        }

        public static void FoldersAreEqual(string expectedFolderName, string actualFolderName)
        {
            FolderExist(expectedFolderName);
            FolderExist(actualFolderName);

            var expectedSubFolders = Directory.GetDirectories(expectedFolderName);
            var actualSubFolders = Directory.GetDirectories(actualFolderName);
            
            Assert.Equal(expectedSubFolders.Length, actualSubFolders.Length);

            for(int i = 0; i< expectedSubFolders.Length; i++)
            {
                var expectedSubFolder = Path.GetFileName(expectedSubFolders[i]);
                var actualSubFolder = Path.GetFileName(actualSubFolders[i]);

                Assert.Equal(expectedSubFolder, actualSubFolder);

                FoldersAreEqual(Path.Combine(expectedFolderName, expectedSubFolder), Path.Combine(actualFolderName, actualSubFolder));
            }

            var expectedFiles = Directory.GetFiles(expectedFolderName);
            var actualFiles = Directory.GetFiles(actualFolderName);
            
            Assert.Equal(expectedFiles.Length, actualFiles.Length);

            for (int i = 0; i < expectedSubFolders.Length; i++)
            {
                var expectedFile = Path.GetFileName(expectedFiles[i]);
                var actualFile = Path.GetFileName(actualFiles[i]);

                Assert.Equal(expectedFile, actualFile);
                FilesAreEqual(Path.Combine(expectedFolderName, expectedFile), Path.Combine(actualFolderName, actualFile));
            }
        }

        public static void FolderExist(string expectedFolderName)
        {
            Assert.True(Directory.Exists(expectedFolderName));
        }

        public static void FilesAreEqual(string expectedFileName, string actualFileName)
        {
            Assert.Equal(File.Exists(expectedFileName), File.Exists(actualFileName));

            const int BYTES_TO_READ = sizeof(Int64);

            var expected = new FileInfo(expectedFileName);
            var actual = new FileInfo(actualFileName);

            Assert.Equal(expected.Length, actual.Length);

            var iterations = (int)Math.Ceiling((double)expected.Length / BYTES_TO_READ);

            using (FileStream actualFileStream = actual.OpenRead())
            using (FileStream expectedFileStream = expected.OpenRead())
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++)
                {
                    actualFileStream.Read(one, 0, BYTES_TO_READ);
                    expectedFileStream.Read(two, 0, BYTES_TO_READ);

                    Assert.Equal(BitConverter.ToInt64(two, 0), BitConverter.ToInt64(one, 0));
                }
            }
        }
    }
}
