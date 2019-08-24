﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;

    public class ScriptParserError
    {
        public Exception Exception { get; }

        public string Message { get; }

        public int Line { get; }

        public int Column { get; }

        internal ScriptParserError(Exception exception, string message, int line, int column)
        {
            Exception = exception;
            Message = message;
            Line = line;
            Column = column;
        }
    }
}
