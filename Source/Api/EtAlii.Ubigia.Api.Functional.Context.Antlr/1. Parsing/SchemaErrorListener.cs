// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using System.IO;
    using Antlr4.Runtime;

    internal class SchemaErrorListener : IAntlrErrorListener<object>
    {
        private readonly List<SchemaParserError> _errors = new();
        public void SyntaxError(TextWriter output, IRecognizer recognizer, object offendingSymbol, int line, int charPositionInLine,
            string msg, RecognitionException e)

        {
            _errors.Add(new SchemaParserError(e, msg, line, charPositionInLine ));
            output.WriteLine($"line {line}:{charPositionInLine} {msg}");
        }

        public SchemaParserError[] ToErrors() => _errors.ToArray();
    }
}
