// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception that indicates that an unauthorized operation has happened on the infrastructure.
    /// </summary>
    [Serializable]
    public class UnauthorizedInfrastructureOperationException : Exception
    {
        /// <summary>
        /// Create a new <see cref="UnauthorizedInfrastructureOperationException"/> instance from the provided serialization parameters.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected UnauthorizedInfrastructureOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Create a new <see cref="UnauthorizedInfrastructureOperationException"/> instance with the provided message.
        /// </summary>
        /// <param name="message"></param>
        public UnauthorizedInfrastructureOperationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Create a new <see cref="UnauthorizedInfrastructureOperationException"/> instance with the provided message and exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public UnauthorizedInfrastructureOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
