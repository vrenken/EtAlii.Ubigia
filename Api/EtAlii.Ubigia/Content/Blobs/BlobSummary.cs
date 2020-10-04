namespace EtAlii.Ubigia
{
    public class BlobSummary 
    {
        public bool IsComplete { get; private set; }

        public ulong[] AvailableParts { get; private set; }

        public ulong TotalParts { get; private set; }

        private BlobSummary()
        {
        }

        public static BlobSummary Create(bool isCompleted, ulong parts, ulong[] availableParts)
        {
            return new BlobSummary
            {
                IsComplete = isCompleted,
                TotalParts = parts,
                AvailableParts = availableParts
            };
        }

        public static BlobSummary Create(bool isCompleted, IBlob blob, ulong[] availableParts)
        {
            return new BlobSummary
            {
                IsComplete = isCompleted,
                TotalParts = blob.TotalParts,
                AvailableParts = availableParts
            };
        }
    }
}
