namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IPathSubjectPartsParser
    {
        string Id { get; }
        LpsParser Parser { get; }
        PathSubjectPart Parse(LpNode node);
        void Validate(PathSubjectPartParserArguments arguments);
    }
}