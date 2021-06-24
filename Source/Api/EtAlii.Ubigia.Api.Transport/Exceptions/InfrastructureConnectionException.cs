// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception that indicates that an exception has happened on the infrastructure.
    /// </summary>
    [Serializable]
    public class InfrastructureConnectionException : Exception
    {
        /// <summary>
        /// Create a new <see cref="InfrastructureConnectionException"/> instance from the provided serialization parameters.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected InfrastructureConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Create a new <see cref="InfrastructureConnectionException"/> instance with the provided message and exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InfrastructureConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
