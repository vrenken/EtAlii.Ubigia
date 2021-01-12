namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    /// <summary>
    /// The interface that abstracts away any GTL specific parser implementation.
    /// </summary>
    public interface IScriptParser
    {
        Subject ParseRootedPath(string text);
        Subject ParseNonRootedPath(string text);

        Subject ParsePath(string text);
        ScriptParseResult Parse(string text);
        ScriptParseResult Parse(string[] text);
    }
}
