namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public interface IFunctionHandlersProvider
    {
        IFunctionHandler[] FunctionHandlers { get; }
    }
}