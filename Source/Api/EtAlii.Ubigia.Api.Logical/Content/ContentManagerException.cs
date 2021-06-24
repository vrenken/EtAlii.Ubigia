// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ContentManagerException : Exception
    {
        protected ContentManagerException(SerializationInfo info, StreamingContext context)
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
