namespace EtAlii.Servus.Api.Functional
{
    using Moppet.Lapa;

    internal interface IPathRelationParserBuilder
    {
        LpsParser CreatePathRelationParser(string name, string symbol);
    }
}