namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IConditionParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        Condition Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}