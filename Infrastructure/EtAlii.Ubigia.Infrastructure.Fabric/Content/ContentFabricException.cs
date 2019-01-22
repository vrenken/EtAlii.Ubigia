namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;

    [Serializable]
    public class ContentFabricException : Exception
    {
        public ContentFabricException()
        {
        }

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