namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InfrastructureConnectionException : Exception
    {
        public InfrastructureConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
