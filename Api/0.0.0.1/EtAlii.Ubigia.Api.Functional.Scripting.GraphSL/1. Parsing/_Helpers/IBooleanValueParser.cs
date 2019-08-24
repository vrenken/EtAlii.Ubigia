namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal interface IBooleanValueParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        bool Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}