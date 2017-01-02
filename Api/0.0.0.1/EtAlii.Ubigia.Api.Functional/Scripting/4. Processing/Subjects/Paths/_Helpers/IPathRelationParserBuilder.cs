namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IPathRelationParserBuilder
    {
        LpsParser CreatePathRelationParser(string name, string symbol);
    }
}