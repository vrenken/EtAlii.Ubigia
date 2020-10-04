namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using Moppet.Lapa;

    internal interface ITimeSpanValueParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        TimeSpan Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}