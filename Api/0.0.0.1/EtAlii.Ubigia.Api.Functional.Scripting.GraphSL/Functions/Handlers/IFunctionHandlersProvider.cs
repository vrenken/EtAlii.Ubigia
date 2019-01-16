namespace EtAlii.Ubigia.Api.Functional
{
    public interface IFunctionHandlersProvider
    {
        IFunctionHandler[] FunctionHandlers { get; }
    }
}