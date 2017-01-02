namespace EtAlii.Servus.Api.Data
{
    using System;

    public class ScriptProcessingException : Exception
    {
        public ScriptProcessingException()
            : base()
        {
        }

        public ScriptProcessingException(string message)
            : base(message)
        {
        }

        public ScriptProcessingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
