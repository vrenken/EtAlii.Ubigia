namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignPathToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignPathToPathOperatorSubProcessor
    {
        public AssignPathToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
