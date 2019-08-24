namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IFunctionHandlerFinder
    {
        IFunctionHandler Find(FunctionSubject functionSubject);
    }
}