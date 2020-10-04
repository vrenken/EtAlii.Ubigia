namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal interface IIntegerValueParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        int Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}