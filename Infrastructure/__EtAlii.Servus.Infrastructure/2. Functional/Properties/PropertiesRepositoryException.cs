namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;

    public class PropertiesRepositoryException : Exception
    {
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