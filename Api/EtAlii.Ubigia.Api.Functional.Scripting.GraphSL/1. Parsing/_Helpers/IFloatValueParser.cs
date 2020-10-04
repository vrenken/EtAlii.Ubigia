namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal interface IFloatValueParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        float Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}