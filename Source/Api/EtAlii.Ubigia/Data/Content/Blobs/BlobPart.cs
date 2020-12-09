namespace EtAlii.Ubigia
{
    public abstract partial class BlobPart
    {
        public bool Stored { get; private set; }

        protected abstract string Name { get; }

        public ulong Id { get; init; }
    }
}
