namespace EtAlii.Ubigia.Api.Functional.Traversal
{
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

    public record GtlLine(string Person, string Text);
}
