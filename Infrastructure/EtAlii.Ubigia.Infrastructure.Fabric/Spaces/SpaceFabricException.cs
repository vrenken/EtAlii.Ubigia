namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Runtime.Serialization;
    using EtAlii.Ubigia.Api;

    [Serializable]
    public class SpaceFabricException : Exception
    {
        protected SpaceFabricException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        
        public SpaceFabricException(string message)
            : base(message)
        {
        }

        public SpaceFabricException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public SpaceFabricException(Identifier source, Identifier target, string message)
            : base(message)
        {
            Data["source"] = source;
            Data["target"] = target;
        }

    }
}