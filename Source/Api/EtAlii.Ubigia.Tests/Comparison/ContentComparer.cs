namespace EtAlii.Ubigia.Tests
{
    using System.Linq;

    public class ContentComparer
    {
        private readonly ByteArrayComparer _byteArrayComparer;

        public ContentComparer(ByteArrayComparer byteArrayComparer)
        {
            _byteArrayComparer = byteArrayComparer;
        }

        public bool AreEqual(IReadOnlyContentDefinition expected, IReadOnlyContentDefinition actual, bool checkSummary)
        {
            if (expected.Checksum != actual.Checksum)
            {
                return false;
            }
            if(expected.Size != actual.Size)
            {
                return false;
            }
            var expectedPartCount = expected.Parts.Count();
            var actualPartCount = actual.Parts.Count();
            if(expectedPartCount != actualPartCount)
            {
                return false;
            }
            for (var i = 0; i < expectedPartCount; i++)
            {
                var expectedPart = expected.Parts.ElementAt(i);
                var actualPart = actual.Parts.ElementAt(i);

                if(!AreEqual(expectedPart, actualPart))
                {
                    return false;
                }
            }

            if (checkSummary)
            {
                if(!AreEqual(expected.Summary, actual.Summary))
                {
                    return false;
                }
            }
            return true;
        }

        public bool AreEqual(BlobSummary expected, BlobSummary actual)
        {
            if (actual == null)
            {
                return false;
            }
            if (expected.IsComplete != actual.IsComplete)
            {
                return false;
            }
            if(expected.TotalParts != actual.TotalParts)
            {
                return false;
            }
            return true;

            //Assert.Equal(expected.AvailableParts, actual.TotalParts);
        }

        public bool AreEqual(IReadOnlyContentDefinitionPart expected, IReadOnlyContentDefinitionPart actual)
        {
            if (actual == null)
            {
                return false;
            }
            if(expected.Id != actual.Id)
            {
                return false;
            }
            if (expected.Checksum != actual.Checksum)
            {
                return false;
            }
            if (expected.Size != actual.Size)
            {
                return false;
            }

            return true;
        }

        public bool AreEqual(IReadOnlyContent expected, IReadOnlyContent actual, bool checkSummary)
        {
            if (expected.TotalParts != actual.TotalParts)
            {
                return false;
            }

            if (checkSummary)
            {
                if (!AreEqual(expected.Summary, actual.Summary))
                {
                    return false;
                }
            }

            return true;
        }

        public bool AreEqual(IReadOnlyContentPart expected, IReadOnlyContentPart actual)
        {
            return _byteArrayComparer.AreEqual(expected.Data, actual.Data);
        }

    }
}
