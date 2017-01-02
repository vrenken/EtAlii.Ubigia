namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public class InfrastructureConnectionException : Exception
    {
        public InfrastructureConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
