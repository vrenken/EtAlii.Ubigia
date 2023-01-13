// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System;
using System.Runtime.Serialization;

[Serializable]
public class GraphTraversalException : Exception
{
    protected GraphTraversalException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public GraphTraversalException(string message)
        : base(message)
    {
    }

    public GraphTraversalException(string message, Exception exception)
        : base(message, exception)
    {
    }
}
