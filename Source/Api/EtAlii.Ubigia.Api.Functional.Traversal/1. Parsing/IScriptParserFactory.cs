namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IScriptParserFactory
    {
        IScriptParser Create(ScriptParserConfiguration configuration);
    }
}
