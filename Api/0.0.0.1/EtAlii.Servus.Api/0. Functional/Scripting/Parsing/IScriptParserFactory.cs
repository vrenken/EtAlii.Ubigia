namespace EtAlii.Servus.Api.Functional
{
    internal interface IScriptParserFactory
    {
        IScriptParser Create(IScriptParserConfiguration configuration);
    }
}