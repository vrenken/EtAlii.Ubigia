namespace EtAlii.Servus.Api.Functional
{
    public interface IFunctionHandlersProvider
    {
        IFunctionHandler[] FunctionHandlers { get; }
    }
}