namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IParameterSetFinder
    {
        ParameterSet Find(FunctionSubject functionSubject, IFunctionHandler functionHandler, ArgumentSet argumentSet);
    }
}