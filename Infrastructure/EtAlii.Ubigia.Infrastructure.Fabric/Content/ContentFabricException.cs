namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ContentFabricException : Exception
    {
        protected ContentFabricException(SerializationInfo info, StreamingContext context)
            : base(info, context)
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