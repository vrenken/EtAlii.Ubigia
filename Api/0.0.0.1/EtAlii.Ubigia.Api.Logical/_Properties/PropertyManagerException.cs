namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    [Serializable]
    public class PropertyManagerException : Exception
    {
        public PropertyManagerException()
        {
        }

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
