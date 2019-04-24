namespace EtAlii.Ubigia.Api.Functional
{
    public class FunctionContext : IFunctionContext
    {
        IPathProcessor IFunctionContext.PathProcessor => _pathProcessor;
        private readonly IPathProcessor _pathProcessor;

        IItemToIdentifierConverter IFunctionContext.ItemToIdentifierConverter => _itemToIdentifierConverter;
        private readonly IItemToIdentifierConverter _itemToIdentifierConverter;

        internal FunctionContext(
            IPathProcessor pathProcessor, 
            IItemToIdentifierConverter itemToIdentifierConverter)
        {
            _pathProcessor = pathProcessor;
            _itemToIdentifierConverter = itemToIdentifierConverter;
        }
    }
}