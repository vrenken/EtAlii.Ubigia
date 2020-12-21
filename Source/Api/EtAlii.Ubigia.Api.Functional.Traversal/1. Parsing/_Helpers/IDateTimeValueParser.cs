namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using Moppet.Lapa;

    internal interface IDateTimeValueParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        DateTime Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}
