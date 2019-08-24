namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal class AssignCombinedToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignCombinedToPathOperatorSubProcessor
    {
        public AssignCombinedToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter, 
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter, 
            IScriptProcessingContext context) 
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, context)
        {
        }
    }
}