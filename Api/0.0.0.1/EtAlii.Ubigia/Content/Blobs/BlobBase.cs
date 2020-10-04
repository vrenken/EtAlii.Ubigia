namespace EtAlii.Ubigia
{
    using System;

    public abstract class BlobBase : IBlob
    {
        public bool Stored { get; internal set; }

        protected internal abstract string Name { get; }

        public BlobSummary Summary { get; internal set; }

        public UInt64 TotalParts { get; set; }
    }
}
