namespace EtAlii.Servus.Api
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
