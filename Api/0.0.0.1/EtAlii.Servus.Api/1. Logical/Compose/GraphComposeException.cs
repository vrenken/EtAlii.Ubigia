namespace EtAlii.Servus.Api.Logical
{
    using System;

    public class GraphComposeException : Exception
    {
        public GraphComposeException(string message)
            : base(message)
        {
        }
    }
}