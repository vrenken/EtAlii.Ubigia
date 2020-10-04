namespace EtAlii.Ubigia.Api.Functional.Scripting
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
