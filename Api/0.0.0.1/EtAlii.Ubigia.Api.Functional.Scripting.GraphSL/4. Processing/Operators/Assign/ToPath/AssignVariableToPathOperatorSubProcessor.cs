namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignVariableToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignVariableToPathOperatorSubProcessor
    {
        public AssignVariableToPathOperatorSubProcessor(
            IToIdentifierConverter toIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
            : base(toIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
