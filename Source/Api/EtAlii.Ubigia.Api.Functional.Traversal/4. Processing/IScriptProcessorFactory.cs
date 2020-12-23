namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface IScriptProcessorFactory
    {
        IScriptProcessor Create(ScriptProcessorConfiguration configuration);
    }
}
