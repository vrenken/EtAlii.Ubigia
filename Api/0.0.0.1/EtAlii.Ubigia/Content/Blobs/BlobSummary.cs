namespace EtAlii.Ubigia
{
    using System;

    public class BlobSummary 
    {
        public bool IsComplete { get; private set; }

        public UInt64[] AvailableParts { get; private set; }

        public UInt64 TotalParts { get; private set; }

        private BlobSummary()
        {
        }

        public static BlobSummary Create(bool isCompleted, UInt64 parts, UInt64[] availableParts)
        {
            return new BlobSummary
            {
                IsComplete = isCompleted,
                TotalParts = parts,
                AvailableParts = availableParts
            };
        }

        public static BlobSummary Create(bool isCompleted, IBlob blob, UInt64[] availableParts)
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
