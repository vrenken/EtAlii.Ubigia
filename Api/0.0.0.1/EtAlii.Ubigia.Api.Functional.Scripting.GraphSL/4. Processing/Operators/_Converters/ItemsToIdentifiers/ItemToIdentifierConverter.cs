namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class ItemToIdentifierConverter : IItemToIdentifierConverter
    {
        private readonly ISelector<object, Func<object, ExecutionScope, Task<Identifier>>> _converterSelector;
        private readonly IProcessingContext _context;

        public ItemToIdentifierConverter(IProcessingContext context)
        {
            _context = context;
            _converterSelector = new Selector<object, Func<object, ExecutionScope, Task<Identifier>>>()
                .Register(item => item is Identifier, ConverterItemAsIdentifierToIdentifier)
                .Register(item => item is IReadOnlyEntry, ConverterItemAsReadOnlyEntryToIdentifier)
                .Register(item => item is INode, ConverterItemAsNodeToIdentifier)
                .Register(item => true, ConverterRootToIdentifier);
        }

        public async Task<Identifier> Convert(object items, ExecutionScope scope)
        {
            var converter = _converterSelector.Select(items);
            return await converter(items, scope);
        }

        private async Task<Identifier> ConverterItemAsIdentifierToIdentifier(object item, ExecutionScope scope)
        {
            return await Task.FromResult((Identifier)item);
        }

        private async Task<Identifier> ConverterItemAsReadOnlyEntryToIdentifier(object item, ExecutionScope scope)
        {
            return await Task.FromResult(((IReadOnlyEntry)item).Id);
        }

        private async Task<Identifier> ConverterItemAsNodeToIdentifier(object item, ExecutionScope scope)
        {
            return await Task.FromResult(((INode)item).Id);
        }

        private async Task<Identifier> ConverterRootToIdentifier(object item, ExecutionScope scope)
        {
            var root = await _context.Logical.Roots.Get(DefaultRoot.Tail);
            return root.Identifier;
        }
    }
}
