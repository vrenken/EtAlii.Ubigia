namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;

    public class ContentRepositoryException : Exception
    {
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