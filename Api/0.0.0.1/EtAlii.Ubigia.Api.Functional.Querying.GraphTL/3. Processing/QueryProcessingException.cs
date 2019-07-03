namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class QueryProcessingException : Exception
    {
        protected QueryProcessingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public QueryProcessingException(string message)
            : base(message)
        {
        }

        public QueryProcessingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
