// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public abstract partial class Blob
    {
        /// <summary>
        /// Returns true when the blob has been stored.
        /// </summary>
        public bool Stored { get; protected set; }

        /// <summary>
        /// Returns the type name of the blob part.
        /// </summary>
        protected abstract string Name { get; }

        public BlobSummary Summary { get; protected set; }

        /// <summary>
        /// Returns the total number of parts that make up the blob.
        /// </summary>
        public ulong TotalParts { get; protected set; }
    }
}
