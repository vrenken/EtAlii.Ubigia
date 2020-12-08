namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class InvalidProfilingOperationException : Exception
    {
        private InvalidProfilingOperationException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public InvalidProfilingOperationException(string message) : base(message)
        {
        }

        public InvalidProfilingOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}