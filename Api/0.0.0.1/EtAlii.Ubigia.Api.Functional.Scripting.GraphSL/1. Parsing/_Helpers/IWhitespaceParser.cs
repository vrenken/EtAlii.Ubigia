namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IWhitespaceParser
    {
        LpsParser Required { get; }
        LpsParser Optional { get; }
    }
}