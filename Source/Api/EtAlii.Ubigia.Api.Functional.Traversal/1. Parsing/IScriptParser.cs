namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IScriptParser
    {
        ScriptParseResult Parse(string text);
        ScriptParseResult Parse(string[] text);
    }
}
