namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IFunctionSubjectArgumentParser
    {
        LpsParser Parser { get; }
        bool CanParse(LpNode node);
        FunctionSubjectArgument Parse(LpNode node);

        bool CanValidate(FunctionSubjectArgument argument);

        void Validate(FunctionSubjectArgument before, FunctionSubjectArgument argument, int parameterIndex, FunctionSubjectArgument after);
    }
}
