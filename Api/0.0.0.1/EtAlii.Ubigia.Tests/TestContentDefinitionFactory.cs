namespace EtAlii.Ubigia.Tests
{
    using System;
    using HashLib;

    public class TestContentDefinitionFactory
    {
        private readonly Random _random;

        public TestContentDefinitionFactory()
        {
            _random = new Random(1234567890);
        }
        public ContentDefinition Create()
        {
            var contentDefinition = new ContentDefinition
            {
                Checksum = (ulong)_random.Next(0, int.MaxValue),
                Size = (ulong)_random.Next(0, int.MaxValue),
            };

            var partIds = (uint)_random.Next(1, 10);

            for (uint partId = 0; partId < partIds; partId++)
            {
                contentDefinition.Parts.Add(CreatePart(partId));
            }

            contentDefinition.TotalParts = partIds;

            return contentDefinition;
        }

        public ContentDefinition Create(byte[] data)
        {
            return Create(new[] { data });
        }

        public ContentDefinition Create(byte[][] datas)
        {
            var hash = HashFactory.Checksum.CreateCRC64_ECMA();
            var contentDefinition = new ContentDefinition
            {
                TotalParts = (ulong)datas.Length
            };

            var totalSize = (ulong)0;
            var totalChecksum = (ulong)0;
            var partId = (ulong)0;
            foreach (var data in datas)
            {
                var checksum = hash.ComputeBytes(data).GetULong();
                var size = (ulong)data.Length;
                contentDefinition.Parts.Add(new ContentDefinitionPart
                {
                    Checksum = checksum,
                    Id = partId++,
                    Size = size,
                });
                totalChecksum ^= checksum;
                totalSize += size;
            }
            contentDefinition.Checksum = totalChecksum;
            contentDefinition.Size = totalSize;
            return contentDefinition;
        }

        public ContentDefinition Create(ulong totalParts)
        {
            var contentDefinition = new ContentDefinition
            {
                Checksum = (ulong)_random.Next(0, int.MaxValue),
                Size = (ulong)_random.Next(0, int.MaxValue),
                TotalParts = totalParts,
            };

            for (ulong partId = 0; partId < totalParts; partId++)
            {
                contentDefinition.Parts.Add(new ContentDefinitionPart
                {
                    Checksum = (ulong)_random.Next(0, int.MaxValue),
                    Id = partId,
                    Size = (ulong)_random.Next(0, int.MaxValue),
                });
            }

            contentDefinition.TotalParts = totalParts;

            return contentDefinition;
        }

        public ContentDefinitionPart CreatePart()
        {
            var partId = (uint)_random.Next(0, int.MaxValue);
            return CreatePart(partId);
        }

        public ContentDefinitionPart CreatePart(ulong partId)
        {
            var contentDefinitionPart = new ContentDefinitionPart
            {
                Id = partId,
                Checksum = (ulong)_random.Next(0, int.MaxValue),
                Size = (ulong)_random.Next(0, int.MaxValue),
            };

            return contentDefinitionPart;
        }

    }
}
