namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IAnnotationNewParser
    {
        string Id { get; }
        
        LpsParser Parser { get; }
        AnnotationNew Parse(LpNode node);

        bool CanParse(LpNode node);

        void Validate(StructureFragment parent, StructureFragment self, AnnotationNew annotation, int depth);
        bool CanValidate(AnnotationNew annotation);

    }
}