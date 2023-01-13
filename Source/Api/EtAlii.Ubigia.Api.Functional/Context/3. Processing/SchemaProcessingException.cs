// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System;
using System.Runtime.Serialization;

[Serializable]
public class SchemaProcessingException : Exception
{
    protected SchemaProcessingException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public SchemaProcessingException(string message)
        : base(message)
    {
    }

    public SchemaProcessingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
