namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface INewLineParser
    {
        LpsParser Required { get; }
        LpsParser Optional { get; }
        LpsParser OptionalMultiple { get; }
    }
}