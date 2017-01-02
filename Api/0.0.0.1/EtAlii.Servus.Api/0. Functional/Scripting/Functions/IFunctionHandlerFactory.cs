namespace EtAlii.Servus.Api.Functional
{
    internal interface IFunctionHandlerFactory
    {
        IFunctionHandler[] CreateDefaults();
    }
}