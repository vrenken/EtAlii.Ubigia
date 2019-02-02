namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class ContentManagerException : Exception
    {
        private ContentManagerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
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
