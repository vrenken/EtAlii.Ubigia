namespace EtAlii.Ubigia.Tests
{
    using System;
    using System.Collections.Generic;
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
            var partIds = (uint)_random.Next(1, 10);

            var parts = new List<ContentDefinitionPart>();
            for (uint partId = 0; partId < partIds; partId++)
            {
                parts.Add(CreatePart(partId));
            }

            var contentDefinition = new ContentDefinition
            {
                Checksum = (ulong)_random.Next(0, int.MaxValue),
                Size = (ulong)_random.Next(0, int.MaxValue),
                Parts = parts.ToArray(),
            };
            Blob.SetTotalParts(contentDefinition, partIds);

            return contentDefinition;
        }

        public ContentDefinition Create(byte[] data)
        {
            return Create(new[] { data });
        }

        public ContentDefinition Create(byte[][] datas)
        {
            var hash = HashFactory.Checksum.CreateCRC64_ECMA();

            var totalSize = (ulong)0;
            var totalChecksum = (ulong)0;
            var partId = (ulong)0;

            var parts = new List<ContentDefinitionPart>();
            foreach (var data in datas)
            {
                var checksum = hash.ComputeBytes(data).GetULong();
                var size = (ulong)data.Length;
                var contentDefinitionPart = new ContentDefinitionPart 
                {
                    Id = partId++,
                    Checksum = checksum,
                    Size = size,
                };
                parts.Add(contentDefinitionPart);
                totalChecksum ^= checksum;
                totalSize += size;
            }
            
            var contentDefinition = new ContentDefinition
            {
                Checksum = totalChecksum,
                Size = totalSize,
                Parts = parts.ToArray(),
            };
            Blob.SetTotalParts(contentDefinition, (ulong)datas.Length);

            return contentDefinition;
        }

        public ContentDefinition Create(ulong totalParts)
        {
            var parts = new List<ContentDefinitionPart>();
            for (ulong partId = 0; partId < totalParts; partId++)
            {
                var contentDefinitionPart = new ContentDefinitionPart 
                {
                    Id = partId,
                    Checksum = (ulong) _random.Next(0, int.MaxValue),
                    Size = (ulong) _random.Next(0, int.MaxValue),
                }; 
                parts.Add(contentDefinitionPart);
            }

            var contentDefinition = new ContentDefinition
            {
                Checksum = (ulong)_random.Next(0, int.MaxValue),
                Size = (ulong)_random.Next(0, int.MaxValue),
                Parts = parts.ToArray(),
            };
            Blob.SetTotalParts(contentDefinition, totalParts);

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
