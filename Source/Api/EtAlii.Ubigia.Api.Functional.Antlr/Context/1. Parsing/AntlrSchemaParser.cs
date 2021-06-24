// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Context
{
    using System;
    using Antlr4.Runtime;
    using EtAlii.Ubigia.Api.Functional.Antlr;
    using EtAlii.Ubigia.Api.Functional.Context;

    internal class AntlrSchemaParser : ISchemaParser
    {
        private readonly IContextValidator _contextValidator;

        public AntlrSchemaParser(IContextValidator contextValidator)
        {
            _contextValidator = contextValidator;
        }

        public SchemaParseResult Parse(string text)
        {
            text ??= string.Empty;

            // The parsing is based on the sequence parts having a newline at the end. For this purpose we add one just in case.
            text += Environment.NewLine;

            var errors = Array.Empty<SchemaParserError>();
            Schema schema;

            try
            {
                var inputStream = new AntlrInputStream(text);
                var gtlLexer = new UbigiaLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(gtlLexer);
                var parser = new UbigiaParser(commonTokenStream);
                var errorListener = new SchemaErrorListener();
                parser.RemoveErrorListeners();
                parser.AddErrorListener(errorListener);
                var context = parser.schema();

                if (parser.NumberOfSyntaxErrors != 0)
                {
                    errors = errorListener.ToErrors();
                    return new SchemaParseResult(string.Join(Environment.NewLine, text), null, errors);
                }
                if (context.exception != null)
                {
                    errors = new[] { new SchemaParserError(context.exception, context.exception.Message, 0, 0) };
                    return new SchemaParseResult(string.Join(Environment.NewLine, text), null, errors);
                }

                var visitor = new UbigiaVisitor();
                schema = visitor.Visit(context) as Schema;

                _contextValidator.Validate(schema);
            }
            catch (Exception e)
            {
                e = new SchemaParserException(e.Message, e);
                errors = new[] { new SchemaParserError(e, e.Message, 0, 0) };
                schema = null;
            }

            return new SchemaParseResult(string.Join(Environment.NewLine, text), schema, errors);
        }

        public SchemaParseResult Parse(string[] text) =>  Parse(string.Join("\n", text));
    }
}
