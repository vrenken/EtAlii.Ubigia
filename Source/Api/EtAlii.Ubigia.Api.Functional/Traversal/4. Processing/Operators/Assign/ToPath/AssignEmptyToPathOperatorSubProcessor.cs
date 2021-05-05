namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class AssignEmptyToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignEmptyToPathOperatorSubProcessor
    {
        public AssignEmptyToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IScriptProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
