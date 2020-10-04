namespace EtAlii.Ubigia.Api
{
    using System;

    public abstract class BlobPartBase : IBlobPart
    {
        public bool Stored { get; internal set; }

        protected internal abstract string Name { get; }

        public UInt64 Id { get; set; }
    }
}
