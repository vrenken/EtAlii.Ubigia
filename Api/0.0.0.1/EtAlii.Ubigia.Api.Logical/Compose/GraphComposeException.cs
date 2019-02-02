namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class GraphComposeException : Exception
    {
        private GraphComposeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public GraphComposeException(string message)
            : base(message)
        {
        }
    }
}