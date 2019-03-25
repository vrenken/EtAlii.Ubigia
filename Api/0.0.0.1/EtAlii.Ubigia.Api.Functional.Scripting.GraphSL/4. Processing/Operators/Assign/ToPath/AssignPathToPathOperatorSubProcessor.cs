namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignPathToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignPathToPathOperatorSubProcessor
    {
        public AssignPathToPathOperatorSubProcessor(
            IToIdentifierConverter toIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
            : base(toIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
