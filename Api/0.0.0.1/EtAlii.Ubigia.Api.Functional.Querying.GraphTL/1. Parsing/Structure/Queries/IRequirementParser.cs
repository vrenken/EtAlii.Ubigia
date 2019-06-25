namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IRequirementParser
    {
        LpsParser Parser { get; }
        string Id { get; }
        Requirement Parse(LpNode node);

    }
}