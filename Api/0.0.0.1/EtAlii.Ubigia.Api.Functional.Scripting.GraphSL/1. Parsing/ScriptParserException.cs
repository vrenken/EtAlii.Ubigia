namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Runtime.Serialization;

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
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
