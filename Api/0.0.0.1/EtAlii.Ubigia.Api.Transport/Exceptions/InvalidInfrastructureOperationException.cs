namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidInfrastructureOperationException : Exception
    {
        protected InvalidInfrastructureOperationException(SerializationInfo info, StreamingContext context)
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
