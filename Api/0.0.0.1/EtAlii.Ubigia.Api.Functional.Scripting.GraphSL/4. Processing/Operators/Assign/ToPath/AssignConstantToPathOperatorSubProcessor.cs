namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal class AssignConstantToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignConstantToPathOperatorSubProcessor
    {
        public AssignConstantToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IScriptProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}
