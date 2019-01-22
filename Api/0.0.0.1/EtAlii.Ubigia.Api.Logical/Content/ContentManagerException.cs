namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    [Serializable]
    public class ContentManagerException : Exception
    {
        public ContentManagerException()
        {
        }

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
