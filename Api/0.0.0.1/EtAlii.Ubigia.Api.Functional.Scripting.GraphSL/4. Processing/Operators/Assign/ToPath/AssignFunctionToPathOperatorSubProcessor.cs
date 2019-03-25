namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignFunctionToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignFunctionToPathOperatorSubProcessor
    {
        public AssignFunctionToPathOperatorSubProcessor(
            IToIdentifierConverter toIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
            : base(toIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
