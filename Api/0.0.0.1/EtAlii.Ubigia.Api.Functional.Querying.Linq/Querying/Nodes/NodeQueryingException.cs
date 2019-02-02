namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class NodeQueryingException : Exception
    {
        private NodeQueryingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private ScriptParseResult ParseResult { get; }

        public NodeQueryingException(string message)
            : base(message)
        {
        }

        public NodeQueryingException(string message, ScriptParseResult parseResult)
            : base(message)
        {
            ParseResult = parseResult;
        }
    }
}