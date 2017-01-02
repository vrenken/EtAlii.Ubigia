namespace EtAlii.Servus.Api.Transport
{
    using System;

    public class InvalidInfrastructureOperationException : Exception
    {
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
