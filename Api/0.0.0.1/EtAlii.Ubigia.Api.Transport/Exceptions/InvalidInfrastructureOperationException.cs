namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class InvalidInfrastructureOperationException : Exception
    {
        private InvalidInfrastructureOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public InvalidInfrastructureOperationException(string message)
            : base(message)
        {
        }

        public InvalidInfrastructureOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
