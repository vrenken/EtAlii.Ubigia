namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface INewLineParser
    {
        LpsChain Required { get; }
        LpsChain Optional { get; }
        LpsParser OptionalMultiple { get; }
    }
}
