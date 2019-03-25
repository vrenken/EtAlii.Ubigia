namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignConstantToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignConstantToPathOperatorSubProcessor
    {
        public AssignConstantToPathOperatorSubProcessor(
            IToIdentifierConverter toIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
            : base(toIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
