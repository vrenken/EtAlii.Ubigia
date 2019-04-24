namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignFunctionToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignFunctionToPathOperatorSubProcessor
    {
        public AssignFunctionToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
