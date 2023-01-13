// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Runtime.Serialization;

[Serializable]
public class ContentRepositoryException : Exception
{
    protected ContentRepositoryException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public ContentRepositoryException(string message)
        : base(message)
    {
    }

    public ContentRepositoryException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
