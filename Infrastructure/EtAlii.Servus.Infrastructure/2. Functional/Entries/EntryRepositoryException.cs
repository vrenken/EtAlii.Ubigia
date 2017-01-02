namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;

    public class EntryRepositoryException : Exception
    {
        public EntryRepositoryException(string message)
            : base(message)
        {
        }

        public EntryRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}