namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;

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
