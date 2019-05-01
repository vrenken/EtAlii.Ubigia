namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IScriptParserFactory
    {
        IScriptParser Create(ScriptParserConfiguration configuration);
    }
}