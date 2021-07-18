// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public abstract partial class Blob
    {
        public bool Stored { get; protected set; }

        protected abstract string Name { get; }

        public BlobSummary Summary { get; protected set; }

        public ulong TotalParts { get; protected set; }
    }
}
