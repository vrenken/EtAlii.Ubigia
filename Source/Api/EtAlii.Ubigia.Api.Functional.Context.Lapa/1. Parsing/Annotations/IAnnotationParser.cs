namespace EtAlii.Ubigia.Api.Functional.Context
{
    using Moppet.Lapa;

    internal interface IAnnotationParser
    {
        string Id { get; }

        LpsParser Parser { get; }
        Annotation Parse(LpNode node);

        bool CanParse(LpNode node);

        void Validate(StructureFragment parent, StructureFragment self, Annotation annotation, int depth);
        bool CanValidate(Annotation annotation);
    }
}
