namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;

    [Serializable]
    public class ContentRepositoryException : Exception
    {
        public ContentRepositoryException()
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