﻿namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System;

    public class InvalidProfilingOperationException : Exception
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