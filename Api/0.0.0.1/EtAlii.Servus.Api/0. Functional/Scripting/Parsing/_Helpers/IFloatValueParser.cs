namespace EtAlii.Servus.Api.Functional
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