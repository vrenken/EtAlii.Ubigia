namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using Antlr4.Runtime;
    using EtAlii.Ubigia.Api.Functional.Antlr;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

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
                var parser = new TraversalScriptParser(commonTokenStream);
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

                var visitor = new TraversalVisitor();
                script = visitor.Visit(context) as Script;

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

        public Subject ParsePath(string text)
        {
            Subject pathSubject;
            try
            {
                var inputStream = new AntlrInputStream(text);
                var gtlLexer = new UbigiaLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(gtlLexer);
                var parser = new TraversalScriptParser(commonTokenStream);
                var errorListener = new ScriptErrorListener();
                parser.RemoveErrorListeners();
                parser.AddErrorListener(errorListener);
                var context = parser.subject_non_rooted_path();

                if (parser.NumberOfSyntaxErrors != 0)
                {
                    var error = errorListener.ToErrorString();
                    throw new ScriptParserException(error);
                }
                if (context.exception != null)
                {
                    throw new ScriptParserException(context.exception.Message, context.exception);
                }

                var visitor = new TraversalVisitor();
                pathSubject = visitor.VisitSubject_non_rooted_path(context) as Subject;

                _traversalValidator.Validate(pathSubject);
            }
            catch (Exception e)
            {
                throw new ScriptParserException(e.Message, e);
            }

            return pathSubject;
        }

        public Subject ParseNonRootedPath(string text)
        {
            Subject pathSubject;
            try
            {
                var inputStream = new AntlrInputStream(text);
                var gtlLexer = new UbigiaLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(gtlLexer);
                var parser = new TraversalScriptParser(commonTokenStream);
                var errorListener = new ScriptErrorListener();
                parser.RemoveErrorListeners();
                parser.AddErrorListener(errorListener);
                var context = parser.subject_non_rooted_path();

                if (parser.NumberOfSyntaxErrors != 0)
                {
                    var error = errorListener.ToErrorString();
                    throw new ScriptParserException(error);
                }
                if (context.exception != null)
                {
                    throw new ScriptParserException(context.exception.Message, context.exception);
                }

                var visitor = new TraversalVisitor();
                pathSubject = visitor.VisitSubject_non_rooted_path(context) as Subject;

                _traversalValidator.Validate(pathSubject);
            }
            catch (Exception e)
            {
                throw new ScriptParserException(e.Message, e);
            }

            return pathSubject;
        }

        public Subject ParseRootedPath(string text)
        {
            Subject pathSubject;
            try
            {
                var inputStream = new AntlrInputStream(text);
                var gtlLexer = new UbigiaLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(gtlLexer);
                var parser = new TraversalScriptParser(commonTokenStream);
                var errorListener = new ScriptErrorListener();
                parser.RemoveErrorListeners();
                parser.AddErrorListener(errorListener);
                var context = parser.subject_rooted_path();

                if (parser.NumberOfSyntaxErrors != 0)
                {
                    var error = errorListener.ToErrorString();
                    throw new ScriptParserException(error);
                }
                if (context.exception != null)
                {
                    throw new ScriptParserException(context.exception.Message, context.exception);
                }

                var visitor = new TraversalVisitor();
                pathSubject = visitor.VisitSubject_rooted_path(context) as Subject;

                _traversalValidator.Validate(pathSubject);
            }
            catch (Exception e)
            {
                throw new ScriptParserException(e.Message, e);
            }

            return pathSubject;
        }
    }
}
