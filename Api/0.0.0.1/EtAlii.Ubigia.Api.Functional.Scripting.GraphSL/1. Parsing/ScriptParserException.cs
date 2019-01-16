namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class ScriptParserException : Exception
    {
        public ScriptParserException()
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
