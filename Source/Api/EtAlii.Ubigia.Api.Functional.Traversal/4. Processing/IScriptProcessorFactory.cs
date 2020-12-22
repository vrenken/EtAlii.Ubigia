namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IScriptProcessorFactory
    {
        IScriptProcessor Create(ScriptProcessorConfiguration configuration);
    }
}
