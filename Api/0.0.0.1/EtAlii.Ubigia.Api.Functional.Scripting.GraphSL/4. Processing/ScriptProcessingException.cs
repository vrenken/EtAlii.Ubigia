namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class ScriptProcessingException : Exception
    {
        private ScriptProcessingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
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
