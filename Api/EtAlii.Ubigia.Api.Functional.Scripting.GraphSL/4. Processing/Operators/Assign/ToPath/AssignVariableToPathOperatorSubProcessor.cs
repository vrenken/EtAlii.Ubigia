namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal class AssignVariableToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignVariableToPathOperatorSubProcessor
    {
        public AssignVariableToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IScriptProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}