namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal interface IPathRelationParserBuilder
    {
        LpsParser CreatePathRelationParser(string name, string symbol);
    }
}