namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignPathToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignPathToPathOperatorSubProcessor
    {
        public AssignPathToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IScriptProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
