namespace EtAlii.Servus.Api.Functional
{

    public interface IScriptParserConfiguration
    {
        IScriptParserExtension[] Extensions { get; }

        IFunctionHandlersProvider FunctionHandlersProvider { get; }

        IScriptParserConfiguration Use(IFunctionHandlersProvider functionHandlersProvider);

        IScriptParserConfiguration Use(IScriptParserExtension[] extensions);
    }
}