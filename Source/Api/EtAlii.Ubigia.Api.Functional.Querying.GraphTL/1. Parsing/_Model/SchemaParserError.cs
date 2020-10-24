namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class SchemaParserError
    {
        public Exception Exception { get; }

        public string Message { get; }

        public int Line { get; }

        public int Column { get; }

        internal SchemaParserError(Exception exception, string message, int line, int column)
        {
            Exception = exception;
            Message = message;
            Line = line;
            Column = column;
        }
    }
}
