namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignFunctionToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignFunctionToPathOperatorSubProcessor
    {
        public AssignFunctionToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IScriptProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
