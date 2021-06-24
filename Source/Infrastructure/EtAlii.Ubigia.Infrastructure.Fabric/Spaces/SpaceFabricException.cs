// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class SpaceFabricException : Exception
    {
        private SpaceFabricException(SerializationInfo info, StreamingContext context)
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

        public SpaceFabricException(in Identifier source, in Identifier target, string message)
            : base(message)
        {
            Data["source"] = source;
            Data["target"] = target;
        }

    }
}