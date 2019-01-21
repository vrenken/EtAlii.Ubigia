﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    [Serializable]
    public class ScriptProcessingException : Exception
    {
        public ScriptProcessingException()
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
