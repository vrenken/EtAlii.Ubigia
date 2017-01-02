namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;

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
