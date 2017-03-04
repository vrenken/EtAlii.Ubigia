namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class ScriptParserError
    {
        public Exception Exception => _exception;
        private readonly Exception _exception;

        public string Message => _message;
        private readonly string _message;

        public int Line => _line;
        private readonly int _line;

        public int Column => _column;
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
