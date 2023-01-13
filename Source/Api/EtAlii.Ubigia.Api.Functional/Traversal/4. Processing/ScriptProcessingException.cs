// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Runtime.Serialization;

[Serializable]
public class ScriptProcessingException : Exception
{
    protected ScriptProcessingException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public ScriptProcessingException(string message)
        : base(message)
    {
    }

    public ScriptProcessingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
