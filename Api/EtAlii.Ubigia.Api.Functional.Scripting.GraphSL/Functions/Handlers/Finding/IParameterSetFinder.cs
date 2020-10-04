namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IParameterSetFinder
    {
        ParameterSet Find(FunctionSubject functionSubject, IFunctionHandler functionHandler, ArgumentSet argumentSet);
    }
}