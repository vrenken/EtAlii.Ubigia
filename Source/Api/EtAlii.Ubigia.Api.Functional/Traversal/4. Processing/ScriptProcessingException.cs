namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ScriptProcessingException : Exception
    {
        protected ScriptProcessingException(SerializationInfo info, StreamingContext context)
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
