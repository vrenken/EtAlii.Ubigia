namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;

    public class ContentFabricException : Exception
    {
        public ContentFabricException(string message)
            : base(message)
        {
        }

        public ContentFabricException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}