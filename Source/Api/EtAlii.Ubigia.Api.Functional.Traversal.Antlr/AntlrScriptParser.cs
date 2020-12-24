namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    /// <summary>
    /// The interface that abstracts away any GTL specific parser implementation.
    /// </summary>
    internal class AntlrScriptParser : IScriptParser
    {
        public ScriptParseResult Parse(string text)
        {
            return null;
        }

        public ScriptParseResult Parse(string[] text)
        {
            return null;
        }
    }

    public class GtlVisitor : GtlBaseVisitor<object>
    {
        public readonly List<GtlLine> Lines = new();

        public override object VisitLine(GtlParser.LineContext context)
        {
            var name = context.name();
            var opinion = context.opinion();

            var line = new GtlLine(name.GetText(), opinion.GetText().Trim('"'));
            Lines.Add(line);

            return line;
        }
    }

    public record GtlLine(string Person, string Text);
}
