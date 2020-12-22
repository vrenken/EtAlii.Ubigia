namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IWhitespaceParser
    {
        LpsParser Required { get; }
        LpsParser Optional { get; }
    }
}
