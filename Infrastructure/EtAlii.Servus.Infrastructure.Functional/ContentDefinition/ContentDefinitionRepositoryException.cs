namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;

    public class ContentDefinitionRepositoryException : Exception
    {
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