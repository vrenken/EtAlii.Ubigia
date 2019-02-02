namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    [Serializable]
    public sealed class ScriptParserException : Exception
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
