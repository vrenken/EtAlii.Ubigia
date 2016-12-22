namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using System;

    internal class InvalidProfilingOperationException : Exception
    {
        public InvalidProfilingOperationException()
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