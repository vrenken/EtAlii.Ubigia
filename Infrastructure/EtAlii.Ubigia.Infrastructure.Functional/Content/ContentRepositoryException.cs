namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class ContentRepositoryException : Exception
    {
        private ContentRepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ContentRepositoryException(string message)
            : base(message)
        {
        }

        public ContentRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}