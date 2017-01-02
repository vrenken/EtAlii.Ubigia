namespace EtAlii.Servus.Api.Functional
{
    internal interface IParameterSetFinder
    {
        ParameterSet Find(FunctionSubject functionSubject, IFunctionHandler functionHandler, ArgumentSet argumentSet);
    }
}