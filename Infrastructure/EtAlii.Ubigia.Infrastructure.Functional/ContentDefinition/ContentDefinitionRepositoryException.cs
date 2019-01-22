namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;

    [Serializable]
    public class ContentDefinitionRepositoryException : Exception
    {
        public ContentDefinitionRepositoryException()
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