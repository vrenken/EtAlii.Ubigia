namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface ISequencePartsParser
    {
        string Id { get; }
        LpsParser Parser { get; }
        SequencePart Parse(LpNode node);
    }
}
