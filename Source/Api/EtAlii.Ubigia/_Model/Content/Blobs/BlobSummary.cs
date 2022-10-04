// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System.IO;

    public sealed record BlobSummary : IBinarySerializable
    {
        public bool IsComplete { get; private set; }

        public ulong[] AvailableParts { get; private set; }

        public ulong TotalParts { get; private set; }

        public static BlobSummary Create(bool isComplete, ulong[] availableParts, ulong totalParts)
        {
            return new BlobSummary
            {
                IsComplete = isComplete,
                AvailableParts = availableParts,
                TotalParts = totalParts
            };
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(IsComplete);
            writer.WriteMany(AvailableParts);
            writer.Write(TotalParts);
        }

        public void Read(BinaryReader reader)
        {
            IsComplete = reader.ReadBoolean();
            AvailableParts = reader.ReadMany<ulong>();
            TotalParts = reader.ReadUInt64();
        }
    }
}
