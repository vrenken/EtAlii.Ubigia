namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IParameterSetFinder
    {
        ParameterSet Find(FunctionSubject functionSubject, IFunctionHandler functionHandler, ArgumentSet argumentSet);
    }
}
