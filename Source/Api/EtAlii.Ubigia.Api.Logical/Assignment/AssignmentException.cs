// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AssignmentException : Exception
    {
        protected AssignmentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public AssignmentException(string message)
            : base(message)
        {
        }
    }
}