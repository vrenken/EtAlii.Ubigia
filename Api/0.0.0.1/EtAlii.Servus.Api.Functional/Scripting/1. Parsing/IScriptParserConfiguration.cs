namespace EtAlii.Servus.Api.Functional
{

    public interface IScriptParserConfiguration
    {
        IScriptParserExtension[] Extensions { get; }

        IScriptParserConfiguration Use(IScriptParserExtension[] extensions);
    }
}