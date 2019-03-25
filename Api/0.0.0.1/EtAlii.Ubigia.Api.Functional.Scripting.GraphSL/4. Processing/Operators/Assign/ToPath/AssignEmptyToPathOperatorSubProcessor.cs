namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignEmptyToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignEmptyToPathOperatorSubProcessor
    {
        public AssignEmptyToPathOperatorSubProcessor(
            IToIdentifierConverter toIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
            : base(toIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
