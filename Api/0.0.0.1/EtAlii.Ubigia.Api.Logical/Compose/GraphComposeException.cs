namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class GraphComposeException : Exception
    {
        protected GraphComposeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public GraphComposeException(string message)
            : base(message)
        {
        }
    }
}