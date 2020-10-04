namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IScriptParserFactory
    {
        IScriptParser Create(ScriptParserConfiguration configuration);
    }
}