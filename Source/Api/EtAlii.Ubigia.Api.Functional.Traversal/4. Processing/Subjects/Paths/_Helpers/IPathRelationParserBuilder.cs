namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IPathRelationParserBuilder
    {
        LpsParser CreatePathRelationParser(string name, string symbol);
    }
}
