namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IFunctionHandlerFinder
    {
        IFunctionHandler Find(FunctionSubject functionSubject);
    }
}