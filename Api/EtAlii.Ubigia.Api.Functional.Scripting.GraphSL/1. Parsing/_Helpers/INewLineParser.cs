namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal interface INewLineParser
    {
        LpsChain Required { get; }
        LpsChain Optional { get; }
        LpsParser OptionalMultiple { get; }
    }
}