namespace EtAlii.Ubigia.Api
{
    public abstract class BlobPartBase : IBlobPart
    {
        public bool Stored { get; internal set; }

        protected internal abstract string Name { get; }

        public ulong Id { get; set; }
    }
}
