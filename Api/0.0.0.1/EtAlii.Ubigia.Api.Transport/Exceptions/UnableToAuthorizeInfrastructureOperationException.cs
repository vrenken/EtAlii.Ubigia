namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public class UnableToAuthorizeInfrastructureOperationException : Exception
    {
        public UnableToAuthorizeInfrastructureOperationException(string message)
            : base(message)
        {
        }

        public UnableToAuthorizeInfrastructureOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
