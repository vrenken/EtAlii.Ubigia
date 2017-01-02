namespace EtAlii.Servus.Api.Functional
{
    using Moppet.Lapa;

    internal interface IFunctionSubjectArgumentsParser
    {
        string Id { get; }
        LpsParser Parser { get; }
        FunctionSubjectArgument Parse(LpNode node);
        void Validate(FunctionSubjectArgument before, FunctionSubjectArgument argument, int parameterIndex, FunctionSubjectArgument after);
    }
}