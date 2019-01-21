namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;

    [Serializable]
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