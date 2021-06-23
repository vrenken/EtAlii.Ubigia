// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public abstract partial class BlobPart
    {
        public bool Stored { get; private set; }

        protected abstract string Name { get; }

        public ulong Id { get; init; }
    }
}
