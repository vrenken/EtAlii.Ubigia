namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using Antlr4.Runtime;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    /// <summary>
    /// The interface that abstracts away any GTL specific parser implementation.
    /// </summary>
    internal class AntlrScriptParser : IScriptParser
    {
        public ScriptParseResult Parse(string text)
        {
            text ??= string.Empty;

            // Newlines and tabs are nasty. Correct them (newlines) or get rid of them (tabs).
            //text = text.Replace("\r\n", "\n");
            //text = text.Replace("\t", " ");

            var errors = Array.Empty<ScriptParserError>();
            Script script = null;

            try
            {
                var inputStream = new AntlrInputStream(text);
                var gtlLexer = new GtlLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(gtlLexer);
                var parser = new GtlParser(commonTokenStream);
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

                var visitor = new GtlVisitor();
                script = visitor.Visit(context) as Script;
            }
            catch (ScriptParserException e)
            {
                errors = new[] { new ScriptParserError(e, e.Message, 0, 0) };
            }
            catch (Exception e)
            {
                e = new ScriptParserException(e.Message, e);
                errors = new[] { new ScriptParserError(e, e.Message, 0, 0) };
            }

            return new ScriptParseResult(string.Join(Environment.NewLine, text), script, errors);
        }

        public ScriptParseResult Parse(string[] text) =>  Parse(string.Join("\n", text));
    }

    public record GtlLine(string Person, string Text);
}
