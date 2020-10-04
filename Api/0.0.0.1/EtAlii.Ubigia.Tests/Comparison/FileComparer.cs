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

            const int BYTES_TO_READ = sizeof(Int64);

            var expected = new FileInfo(expectedFileName);
            var actual = new FileInfo(actualFileName);

            if(expected.Length != actual.Length)
            {
                return false;
            }

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

                    if (BitConverter.ToInt64(two, 0) != BitConverter.ToInt64(one, 0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
