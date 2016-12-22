namespace EtAlii.Servus.Api.Logical
{
    using System;

    public class PropertyManagerException : Exception
    {
        public PropertyManagerException(string message)
            : base(message)
        {
        }

        public PropertyManagerException(string message, Exception e)
            : base(message, e)
        {
        }
    }
}
