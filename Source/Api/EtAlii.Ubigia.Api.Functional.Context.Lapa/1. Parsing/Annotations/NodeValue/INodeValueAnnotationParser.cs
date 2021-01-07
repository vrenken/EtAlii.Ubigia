namespace EtAlii.Ubigia.Api.Functional.Context
{
    using Moppet.Lapa;

    internal interface INodeValueAnnotationParser
    {
        string Id { get; }

        LpsParser Parser { get; }
        NodeValueAnnotation Parse(LpNode node);

        bool CanParse(LpNode node);

        void Validate(StructureFragment parent, StructureFragment self, NodeValueAnnotation annotation, int depth);
        bool CanValidate(NodeValueAnnotation annotation);
    }
}
