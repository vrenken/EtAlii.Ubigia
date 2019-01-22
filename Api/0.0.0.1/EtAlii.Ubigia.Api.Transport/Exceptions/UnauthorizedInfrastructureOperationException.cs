namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    [Serializable]
    public class UnauthorizedInfrastructureOperationException : Exception
    {
        public UnauthorizedInfrastructureOperationException()
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
