// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System;
using System.Runtime.Serialization;

[Serializable]
public class SchemaParserException : Exception
{
    protected SchemaParserException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public SchemaParserException(string message)
        : base(message)
    {
    }

    public SchemaParserException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
