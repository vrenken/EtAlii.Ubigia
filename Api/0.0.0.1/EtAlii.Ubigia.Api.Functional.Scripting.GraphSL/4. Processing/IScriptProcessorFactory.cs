namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IScriptProcessorFactory
    {
        IScriptProcessor Create(ScriptProcessorConfiguration configuration);
    }
}
