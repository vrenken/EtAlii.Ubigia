namespace EtAlii.Ubigia
{
    public abstract class BlobBase : IBlob
    {
        public bool Stored { get; internal set; }

        protected internal abstract string Name { get; }

        public BlobSummary Summary { get; internal set; }

        public ulong TotalParts { get; set; }
    }
}
