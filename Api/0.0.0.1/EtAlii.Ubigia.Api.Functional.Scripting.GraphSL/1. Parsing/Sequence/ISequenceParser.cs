namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface ISequenceParser
    {
        string Id { get; }
        LpsParser Parser { get; }
        Sequence Parse(string text);
    }
}