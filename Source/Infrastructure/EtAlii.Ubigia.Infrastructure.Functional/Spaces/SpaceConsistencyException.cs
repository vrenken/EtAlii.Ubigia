// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Runtime.Serialization;

[Serializable]
public sealed class SpaceConsistencyException : Exception
{
    private SpaceConsistencyException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public SpaceConsistencyException(in Identifier source, in Identifier target, string message)
        : base(message)
    {
        Data["source"] = source;
        Data["target"] = target;
    }
}
