namespace EtAlii.Servus.Api.Functional
{
    public interface IFunctionHandlerConfiguration
    {
        IFunctionHandler FunctionHandler { get; }
        ArgumentSet[] ArgumentSets { get; }
        string Name { get; }
    }
}