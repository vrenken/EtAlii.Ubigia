namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IFunctionSubjectArgumentsParser
    {
        string Id { get; }
        LpsParser Parser { get; }
        FunctionSubjectArgument Parse(LpNode node);
    }
}
