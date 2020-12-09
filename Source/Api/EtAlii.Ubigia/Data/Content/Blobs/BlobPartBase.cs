﻿namespace EtAlii.Ubigia
{
    public abstract class BlobPartBase
    {
        public bool Stored { get; internal set; }

        protected internal abstract string Name { get; }

        public ulong Id { get; set; }
    }
}
