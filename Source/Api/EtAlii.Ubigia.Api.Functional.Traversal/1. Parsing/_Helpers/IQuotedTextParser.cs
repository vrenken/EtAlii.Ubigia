namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IQuotedTextParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        string Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}
