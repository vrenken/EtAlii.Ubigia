namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InfrastructureConnectionException : Exception
    {
        public InfrastructureConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        public InfrastructureConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
