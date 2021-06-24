// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    using System;
    using Antlr4.Runtime;
    using EtAlii.Ubigia.Api.Functional.Antlr;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    /// <summary>
    /// The interface that abstracts away any GTL specific parser implementation.
    /// </summary>
    internal class AntlrScriptParser : IScriptParser
    {
        private readonly ITraversalValidator _traversalValidator;

        public AntlrScriptParser(ITraversalValidator traversalValidator)
        {
            _traversalValidator = traversalValidator;
        }

        public ScriptParseResult Parse(string text)
        {
            text ??= string.Empty;

            // The parsing is based on the sequence parts having a newline at the end. For this purpose we add one just in case.
            text += Environment.NewLine;

            var errors = Array.Empty<ScriptParserError>();
            Script script;

            try
            {
                var inputStream = new AntlrInputStream(text);
                var gtlLexer = new UbigiaLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(gtlLexer);
                var parser = new UbigiaParser(commonTokenStream);
                var errorListener = new ScriptErrorListener();
                parser.RemoveErrorListeners();
                parser.AddErrorListener(errorListener);
                var context = parser.script();

                if (parser.NumberOfSyntaxErrors != 0)
                {
                    var error = errorListener.ToErrorString();
                    throw new ScriptParserException(error);
                }
                if (context.exception != null)
                {
                    throw new ScriptParserException(context.exception.Message, context.exception);
                }

                var visitor = new UbigiaVisitor();
                script = visitor.VisitScript(context) as Script;

                _traversalValidator.Validate(script);
            }
            catch (ScriptParserException e)
            {
                errors = new[] { new ScriptParserError(e, e.Message, 0, 0) };
                script = null;
            }
            catch (Exception e)
            {
                e = new ScriptParserException(e.Message, e);
                errors = new[] { new ScriptParserError(e, e.Message, 0, 0) };
                script = null;
            }

            return new ScriptParseResult(string.Join(Environment.NewLine, text), script, errors);
        }

        public ScriptParseResult Parse(string[] text) =>  Parse(string.Join("\n", text));
    }
}
