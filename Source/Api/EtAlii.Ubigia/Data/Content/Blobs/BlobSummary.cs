namespace EtAlii.Ubigia
{
    public record BlobSummary 
    {
        public bool IsComplete { get; init; }

        public ulong[] AvailableParts { get; init; }

        public ulong TotalParts { get; init; }
    }
}
