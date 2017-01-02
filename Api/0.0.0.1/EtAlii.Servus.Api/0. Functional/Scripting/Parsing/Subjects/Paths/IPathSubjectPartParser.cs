namespace EtAlii.Servus.Api.Functional
{
    using Moppet.Lapa;

    internal interface IPathSubjectPartParser
    {
        LpsParser Parser { get; }
        bool CanParse(LpNode node);
        PathSubjectPart Parse(LpNode node);

        bool CanValidate(PathSubjectPart part);
        void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after);

    }
}