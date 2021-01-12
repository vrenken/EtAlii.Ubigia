namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using Antlr4.Runtime;
    using EtAlii.Ubigia.Api.Functional.Antlr;
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal class AntlrSchemaParser : ISchemaParser
    {
        private readonly IContextValidator _contextValidator;
        private readonly IPathParser _pathParser;

        public AntlrSchemaParser(
            IContextValidator contextValidator,
            IPathParser pathParser)
        {
            _contextValidator = contextValidator;
            _pathParser = pathParser;
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
                var parser = new ContextSchemaParser(commonTokenStream);
                var errorListener = new SchemaErrorListener();
                parser.RemoveErrorListeners();
                parser.AddErrorListener(errorListener);
                var context = parser.schema();

                if (parser.NumberOfSyntaxErrors != 0)
                {
                    var error = errorListener.ToErrorString();
                    throw new SchemaParserException(error);
                }
                if (context.exception != null)
                {
                    throw new SchemaParserException(context.exception.Message, context.exception);
                }

                var visitor = new ContextVisitor(_pathParser);
                schema = visitor.Visit(context) as Schema;

                _contextValidator.Validate(schema);
            }
            catch (SchemaParserException e)
            {
                errors = new[] { new SchemaParserError(e, e.Message, 0, 0) };
                schema = null;
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
