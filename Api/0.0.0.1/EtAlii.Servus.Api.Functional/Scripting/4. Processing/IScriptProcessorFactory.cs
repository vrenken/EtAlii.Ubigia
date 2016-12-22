namespace EtAlii.Servus.Api.Functional
{
    internal interface IScriptProcessorFactory
    {
        IScriptProcessor Create(IScriptProcessorConfiguration configuration);
    }
}
