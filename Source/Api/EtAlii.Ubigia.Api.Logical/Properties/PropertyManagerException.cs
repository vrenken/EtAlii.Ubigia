// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class PropertyManagerException : Exception
    {
        protected PropertyManagerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
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
