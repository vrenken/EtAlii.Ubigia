// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public abstract partial class BlobPart
    {
        /// <summary>
        /// Returns true when the blob part has been stored.
        /// </summary>
        public bool Stored { get; private set; }

        /// <summary>
        /// Returns the type name of the blob part.
        /// </summary>
        protected abstract string Name { get; }

        public ulong Id { get; init; }
    }
}
