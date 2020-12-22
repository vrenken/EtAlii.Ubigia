namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class AssignVariableToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignVariableToPathOperatorSubProcessor
    {
        public AssignVariableToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IScriptProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
