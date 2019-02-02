namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class ScriptParserException : Exception
    {
        private ScriptParserException(SerializationInfo info, StreamingContext context)
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
