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

                // Act.
                var context = parser.script();
                var visitor = new GtlVisitor();
                script = visitor.Visit(context) as Script;
            }
            catch (Exception e)
            {
                errors = new[] { new ScriptParserError(e, e.Message, 0, 0) };
            }

            return new ScriptParseResult(string.Join(Environment.NewLine, text), script, errors);
        }

        public ScriptParseResult Parse(string[] text) =>  Parse(string.Join("\n", text));
    }

    public record GtlLine(string Person, string Text);
}
