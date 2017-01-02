namespace EtAlii.Servus.Api.Transport
{
    using System;

    public class UnauthorizedInfrastructureOperationException : Exception
    {
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
