namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Api;

    public class SpaceFabricException : Exception
    {
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