namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IScriptProcessorFactory
    {
        IScriptProcessor Create(IScriptProcessorConfiguration configuration);
    }
}
