namespace EtAlii.Ubigia.Tests
{
    using System;
    using System.IO;

    public class FileComparer
    {
        public bool AreEqual(string expectedFileName, string actualFileName)
        {
            if (File.Exists(expectedFileName) != File.Exists(actualFileName))
            {
                return false;
            }

            const int bytesToRead = sizeof(long);

            var expected = new FileInfo(expectedFileName!);
            var actual = new FileInfo(actualFileName!);

            if(expected.Length != actual.Length)
            {
                return false;
            }

            var iterations = (int)Math.Ceiling((double)expected.Length / bytesToRead);

            using var actualFileStream = actual.OpenRead();
            using var expectedFileStream = expected.OpenRead();

            var one = new byte[bytesToRead];
            var two = new byte[bytesToRead];

            for (var i = 0; i < iterations; i++)
            {
                actualFileStream.Read(one, 0, bytesToRead);
                expectedFileStream.Read(two, 0, bytesToRead);

                if (BitConverter.ToInt64(two, 0) != BitConverter.ToInt64(one, 0))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
