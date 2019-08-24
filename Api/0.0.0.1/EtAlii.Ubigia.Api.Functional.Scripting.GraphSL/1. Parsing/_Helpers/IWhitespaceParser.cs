namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal interface IWhitespaceParser
    {
        LpsParser Required { get; }
        LpsParser Optional { get; }
    }
}