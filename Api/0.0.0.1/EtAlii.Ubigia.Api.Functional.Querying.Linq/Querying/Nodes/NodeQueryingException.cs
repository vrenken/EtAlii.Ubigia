namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class NodeQueryingException : Exception
    {
        [NonSerialized] 
        private ScriptParseResult _parseResult;

        protected NodeQueryingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public NodeQueryingException(string message)
            : base(message)
        {
        }

        public NodeQueryingException(string message, ScriptParseResult parseResult)
            : base(message)
        {
            _parseResult = parseResult;
        }
    }
}