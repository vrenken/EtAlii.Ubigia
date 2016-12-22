namespace EtAlii.Servus.Api.Functional
{
    internal interface IFunctionHandlerFinder
    {
        IFunctionHandler Find(FunctionSubject functionSubject);
    }
}