namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class SchemaParserException : Exception
    {
        protected SchemaParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public SchemaParserException(string message)
            : base(message)
        {
        }

        public SchemaParserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
