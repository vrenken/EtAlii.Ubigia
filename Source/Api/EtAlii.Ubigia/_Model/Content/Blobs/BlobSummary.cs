// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public sealed record BlobSummary
    {
        public bool IsComplete { get; init; }

        public ulong[] AvailableParts { get; init; }

        public ulong TotalParts { get; init; }
    }
}
