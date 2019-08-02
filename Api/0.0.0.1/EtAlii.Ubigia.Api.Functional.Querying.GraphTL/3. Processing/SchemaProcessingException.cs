namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class SchemaProcessingException : Exception
    {
        protected SchemaProcessingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public SchemaProcessingException(string message)
            : base(message)
        {
        }

        public SchemaProcessingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
