// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
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
