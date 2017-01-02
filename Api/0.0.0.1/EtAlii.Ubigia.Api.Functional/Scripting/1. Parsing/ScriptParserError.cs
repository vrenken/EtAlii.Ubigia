namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class ScriptParserError
    {
        public Exception Exception { get { return _exception; } }
        private readonly Exception _exception;

        public string Message { get { return _message; } }
        private readonly string _message;

        public int Line { get { return _line; } }
        private readonly int _line;

        public int Column { get { return _column; } }
        private readonly int _column;

        internal ScriptParserError(Exception exception, string message, int line, int column)
        {
            _exception = exception;
            _message = message;
            _line = line;
            _column = column;
        }
    }
}
