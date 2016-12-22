namespace EtAlii.Servus.Api.Functional
{
    internal interface IScriptParser
    {
        ScriptParseResult Parse(string text);
        ScriptParseResult Parse(string[] text);
    }
}
