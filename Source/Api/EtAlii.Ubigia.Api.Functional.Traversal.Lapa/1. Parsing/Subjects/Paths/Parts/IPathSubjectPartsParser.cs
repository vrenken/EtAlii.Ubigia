namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IPathSubjectPartsParser
    {
        string Id { get; }
        LpsParser Parser { get; }
        PathSubjectPart Parse(LpNode node);
    }
}
