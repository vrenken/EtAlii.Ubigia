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
                var oneLength = actualFileStream.Read(one, 0, bytesToRead);
                var twoLength = expectedFileStream.Read(two, 0, bytesToRead);

                // Only comparing the lengths is sufficient. If there are fewer bytes then
                // the previous bytes are used. As these will be the same the BitConverter comparison
                // will work as expected.
                if (oneLength != twoLength)
                {
                    throw new InvalidOperationException("Incompatible number of bytes read");
                }

                if (BitConverter.ToInt64(two, 0) != BitConverter.ToInt64(one, 0))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
