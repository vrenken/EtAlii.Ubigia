namespace EtAlii.Ubigia
{
    public abstract partial class BlobPart
    {
        public bool Stored { get; internal set; }

        protected internal abstract string Name { get; }

        public ulong Id { get; set; }
    }
}
