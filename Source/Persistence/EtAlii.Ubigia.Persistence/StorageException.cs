// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class StorageException : Exception
    {
        private StorageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public StorageException(string message)
            : base(message)
        {
        }

        public StorageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
