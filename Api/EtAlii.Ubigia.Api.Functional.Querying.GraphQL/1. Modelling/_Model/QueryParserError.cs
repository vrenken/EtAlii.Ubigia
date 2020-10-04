namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;

    public class QueryParserError
    {
        public Exception Exception { get; }

        public string Message { get; }

        public int Line { get; }

        public int Column { get; }

        internal QueryParserError(Exception exception, string message, int line, int column)
        {
            Exception = exception;
            Message = message;
            Line = line;
            Column = column;
        }
    }
}
