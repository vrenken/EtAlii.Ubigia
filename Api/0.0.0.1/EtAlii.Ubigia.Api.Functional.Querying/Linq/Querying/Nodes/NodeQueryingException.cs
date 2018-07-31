namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class NodeQueryingException : Exception
    {
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