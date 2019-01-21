namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    [Serializable]
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
