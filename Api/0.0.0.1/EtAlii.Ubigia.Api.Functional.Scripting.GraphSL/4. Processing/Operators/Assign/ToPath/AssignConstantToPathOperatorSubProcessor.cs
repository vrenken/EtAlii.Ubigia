namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignConstantToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignConstantToPathOperatorSubProcessor
    {
        public AssignConstantToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
