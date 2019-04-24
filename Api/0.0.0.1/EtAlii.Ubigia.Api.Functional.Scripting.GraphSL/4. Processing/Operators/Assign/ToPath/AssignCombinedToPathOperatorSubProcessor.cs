namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignCombinedToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignCombinedToPathOperatorSubProcessor
    {
        public AssignCombinedToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter, 
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter, 
            IProcessingContext context) 
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}