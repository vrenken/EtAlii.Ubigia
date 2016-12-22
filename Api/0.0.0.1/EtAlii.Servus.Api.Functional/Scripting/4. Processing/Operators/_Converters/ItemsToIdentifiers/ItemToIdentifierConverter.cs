namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class ItemToIdentifierConverter : IItemToIdentifierConverter
    {
        private readonly ISelector<object, Func<object, ExecutionScope, Task<Identifier>>> _converterselector;
        private readonly IProcessingContext _context;

        public ItemToIdentifierConverter(IProcessingContext context)
        {
            _context = context;
            _converterselector = new Selector<object, Func<object, ExecutionScope, Task<Identifier>>>()
                .Register(item => item is Identifier, ConverterItemAsIdentifierToIdentifier)
                .Register(item => item is IReadOnlyEntry, ConverterItemAsReadOnlyEntryToIdentifier)
                .Register(item => item is INode, ConverterItemAsNodeToIdentifier)
                .Register(item => true, ConverterRootToIdentifier);
        }

        public async Task<Identifier> Convert(object items, ExecutionScope scope)
        {
            var converter = _converterselector.Select(items);
            return await converter(items, scope);
        }

        private Task<Identifier> ConverterItemAsIdentifierToIdentifier(object item, ExecutionScope scope)
        {
            return Task.FromResult((Identifier)item);
        }

        private Task<Identifier> ConverterItemAsReadOnlyEntryToIdentifier(object item, ExecutionScope scope)
        {
            return Task.FromResult(((IReadOnlyEntry)item).Id);
        }

        private Task<Identifier> ConverterItemAsNodeToIdentifier(object item, ExecutionScope scope)
        {
            return Task.FromResult(((INode)item).Id);
        }

        private async Task<Identifier> ConverterRootToIdentifier(object item, ExecutionScope scope)
        {
            var root = await _context.Logical.Roots.Get(DefaultRoot.Tail);
            return root.Identifier;
        }
    }
}
