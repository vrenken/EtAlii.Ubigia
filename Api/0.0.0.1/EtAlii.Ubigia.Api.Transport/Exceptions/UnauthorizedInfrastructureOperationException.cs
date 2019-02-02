namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class UnauthorizedInfrastructureOperationException : Exception
    {
        private UnauthorizedInfrastructureOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public UnauthorizedInfrastructureOperationException(string message)
            : base(message)
        {
        }

        public UnauthorizedInfrastructureOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
