namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class PropertiesRepositoryException : Exception
    {
        private PropertiesRepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public PropertiesRepositoryException(string message)
            : base(message)
        {
        }

        public PropertiesRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}