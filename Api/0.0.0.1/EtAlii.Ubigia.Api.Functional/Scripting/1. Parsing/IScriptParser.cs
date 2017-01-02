namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IScriptParser
    {
        ScriptParseResult Parse(string text);
        ScriptParseResult Parse(string[] text);
    }
}
