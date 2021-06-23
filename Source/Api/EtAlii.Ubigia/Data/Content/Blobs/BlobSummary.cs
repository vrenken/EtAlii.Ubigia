// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public record BlobSummary 
    {
        public bool IsComplete { get; init; }

        public ulong[] AvailableParts { get; init; }

        public ulong TotalParts { get; init; }
    }
}
