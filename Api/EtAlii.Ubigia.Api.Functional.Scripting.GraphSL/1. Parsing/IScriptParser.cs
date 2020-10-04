namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IScriptParser
    {
        ScriptParseResult Parse(string text);
        ScriptParseResult Parse(string[] text);
    }
}
