namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IFunctionHandlerFinder
    {
        IFunctionHandler Find(FunctionSubject functionSubject);
    }
}
