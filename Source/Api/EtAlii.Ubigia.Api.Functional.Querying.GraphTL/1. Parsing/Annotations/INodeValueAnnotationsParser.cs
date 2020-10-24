namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface INodeValueAnnotationsParser
    {
        string Id { get; }
        
        LpsParser Parser { get; }
        NodeValueAnnotation Parse(LpNode node);

        bool CanParse(LpNode node);

        void Validate(StructureFragment parent, StructureFragment self, NodeValueAnnotation annotation, int depth);
        bool CanValidate(Annotation annotation);
    }
}