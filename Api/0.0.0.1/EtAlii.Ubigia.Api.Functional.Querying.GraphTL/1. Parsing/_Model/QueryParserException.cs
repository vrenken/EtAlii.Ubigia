namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class QueryParserException : Exception
    {
        protected QueryParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public QueryParserException(string message)
            : base(message)
        {
        }

        public QueryParserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
