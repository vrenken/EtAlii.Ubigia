namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ContentDefinitionRepositoryException : Exception
    {
        protected ContentDefinitionRepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        
        public ContentDefinitionRepositoryException(string message)
            : base(message)
        {
        }

        public ContentDefinitionRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}