namespace EtAlii.Ubigia.Storage.Tests
{
    using System;
    using EtAlii.Ubigia.Api;
    using HashLib;

    public static class TestContentDefinition
    {
        public static ContentDefinition Create()
        {
            var rnd = new Random();
            var contentDefinition = new ContentDefinition
            {
                Checksum = (UInt64)rnd.Next(0, int.MaxValue),
                Size = (UInt64)rnd.Next(0, int.MaxValue),
            };

            var partIds = (UInt32)rnd.Next(1, 10);

            for (UInt32 partId = 0; partId < partIds; partId++)
            {
                contentDefinition.Parts.Add(CreatePart(partId));
            }

            contentDefinition.TotalParts = partIds;

            return contentDefinition;
        }

        public static ContentDefinition Create(byte[] data)
        {
            return Create(new byte[][] { data });
        }

        public static ContentDefinition Create(byte[][] datas)
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

        public static ContentDefinition Create(UInt64 totalParts = 0)
        {
            var rnd = new Random();
            var contentDefinition = new ContentDefinition
            {
                Checksum = (UInt64)rnd.Next(0, int.MaxValue),
                Size = (UInt64)rnd.Next(0, int.MaxValue),
                TotalParts = totalParts,
            };

            for (UInt64 partId = 0; partId < totalParts; partId++)
            {
                contentDefinition.Parts.Add(new ContentDefinitionPart
                {
                    Checksum = (UInt64)rnd.Next(0, int.MaxValue),
                    Id = partId,
                    Size = (UInt64)rnd.Next(0, int.MaxValue),
                });
            }

            contentDefinition.TotalParts = totalParts;

            return contentDefinition;
        }

        public static ContentDefinitionPart CreatePart()
        {
            var rnd = new Random();
            var partId = (UInt32)rnd.Next(0, int.MaxValue);
            return CreatePart(partId);
        }

        public static ContentDefinitionPart CreatePart(UInt32 partId)
        {
            var rnd = new Random();
            var contentDefinitionPart = new ContentDefinitionPart
            {
                Id = partId,
                Checksum = (UInt64)rnd.Next(0, int.MaxValue),
                Size = (UInt64)rnd.Next(0, int.MaxValue),
            };

            return contentDefinitionPart;
        }

    }
}
