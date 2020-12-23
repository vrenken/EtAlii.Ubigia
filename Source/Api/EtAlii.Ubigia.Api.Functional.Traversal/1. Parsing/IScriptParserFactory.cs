namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface IScriptParserFactory
    {
        IScriptParser Create(ScriptParserConfiguration configuration);
    }
}
