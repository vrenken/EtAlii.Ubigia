namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal interface IPathRelationParserHelper
    {
        LpsParser CreatePathRelationParser(string name, string symbol);
    }
}