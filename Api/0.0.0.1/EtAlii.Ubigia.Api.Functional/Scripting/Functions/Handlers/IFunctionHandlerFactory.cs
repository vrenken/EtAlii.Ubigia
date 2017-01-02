namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IFunctionHandlerFactory
    {
        IFunctionHandler[] CreateDefaults();
    }
}