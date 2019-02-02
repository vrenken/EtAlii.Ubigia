namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Runtime.Serialization;
    using EtAlii.Ubigia.Api;

    [Serializable]
    public sealed class SpaceConsistencyException : Exception
    {
        private SpaceConsistencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public SpaceConsistencyException(Identifier source, Identifier target, string message)
            : base(message)
        {
            Data["source"] = source;
            Data["target"] = target;
        }
    }
}
