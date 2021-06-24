// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ScriptParserException : Exception
    {
        protected ScriptParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ScriptParserException(string message)
            : base(message)
        {
        }

        public ScriptParserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
