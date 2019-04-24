namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignVariableToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignVariableToPathOperatorSubProcessor
    {
        public AssignVariableToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
