namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public class ContentManagerException : Exception
    {
        public ContentManagerException(string message)
            : base(message)
        {
        }

        public ContentManagerException(string message, Exception e)
            : base(message, e)
        {
        }
    }
}
