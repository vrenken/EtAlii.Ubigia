namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal interface IPathRelationParserBuilder
    {
        LpsParser CreatePathRelationParser(string name, string symbol);
    }
}