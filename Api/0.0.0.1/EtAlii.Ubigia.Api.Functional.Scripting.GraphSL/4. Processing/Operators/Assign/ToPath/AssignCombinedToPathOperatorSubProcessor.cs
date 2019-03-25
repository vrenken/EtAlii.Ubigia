namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignCombinedToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignCombinedToPathOperatorSubProcessor
    {
        public AssignCombinedToPathOperatorSubProcessor(
            IToIdentifierConverter toIdentifierConverter, 
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter, 
            IProcessingContext context) 
            : base(toIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}