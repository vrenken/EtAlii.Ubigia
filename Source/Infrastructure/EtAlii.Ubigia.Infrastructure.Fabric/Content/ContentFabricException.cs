// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using System.Runtime.Serialization;

[Serializable]
public class ContentFabricException : Exception
{
    protected ContentFabricException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public ContentFabricException(string message)
        : base(message)
    {
    }

    public ContentFabricException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
