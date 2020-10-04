namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IScriptProcessorFactory
    {
        IScriptProcessor Create(ScriptProcessorConfiguration configuration);
    }
}
