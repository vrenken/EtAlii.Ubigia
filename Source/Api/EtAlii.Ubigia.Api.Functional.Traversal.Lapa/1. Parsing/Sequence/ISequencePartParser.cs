namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface ISequencePartParser
    {
        LpsParser Parser { get; }
        SequencePart Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}
