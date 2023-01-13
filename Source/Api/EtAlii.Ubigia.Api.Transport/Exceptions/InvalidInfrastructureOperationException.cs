// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System;
using System.Runtime.Serialization;

/// <summary>
/// An exception that indicates that an invalid operation has happened on the infrastructure.
/// </summary>
[Serializable]
public class InvalidInfrastructureOperationException : Exception
{
    /// <summary>
    /// Create a new <see cref="InvalidInfrastructureOperationException"/> instance from the provided serialization parameters.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected InvalidInfrastructureOperationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Create a new <see cref="InvalidInfrastructureOperationException"/> instance with the provided message.
    /// </summary>
    /// <param name="message"></param>
    public InvalidInfrastructureOperationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Create a new <see cref="InvalidInfrastructureOperationException"/> instance with the provided message and exception.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public InvalidInfrastructureOperationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
