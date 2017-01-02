namespace EtAlii.Ubigia.Api
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    public class BlobSummary 
    {
        public bool IsComplete { get { return _isComplete; } internal set { _isComplete = value; } }
        private bool _isComplete;

        public UInt64[] AvailableParts { get { return _availableParts; } internal set { _availableParts = value; } }
        private UInt64[] _availableParts;

        public UInt64 TotalParts { get { return _totalParts; } internal set { _totalParts = value; } }
        private UInt64 _totalParts;

        public BlobSummary()
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
