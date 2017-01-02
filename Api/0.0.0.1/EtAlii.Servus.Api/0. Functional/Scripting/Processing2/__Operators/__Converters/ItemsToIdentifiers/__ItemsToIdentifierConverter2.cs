//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Threading.Tasks;
//    using EtAlii.xTechnology.Structure;

//    internal class ItemsToIdentifierConverter2 : IItemsToIdentifiersConverter2
//    {
//        private readonly ISelector<object, Func<object, ExecutionScope, Task<Identifier[]>>> _converterselector;
//        private readonly IProcessingContext _context;

//        public ItemsToIdentifierConverter2(IProcessingContext context)
//        {
//            _context = context;
//            _converterselector = new Selector<object, Func<object, ExecutionScope, Task<Identifier[]>>>()
//                .Register(items => items is Identifier, ConverterItemsAsIdentifierToIdentifiers)
//                .Register(items => items is IReadOnlyEntry, ConverterItemsAsReadOnlyEntryToIdentifiers)
//                .Register(items => items is INode, ConverterItemsAsNodeToIdentifiers)
//                .Register(items => true, async (o, e) => await ConverterRootToIdentifiers(o,e));
//        }

//        public async Task<Identifier[]> Convert(object item, ExecutionScope scope)
//        {
//            var converter = _converterselector.Select(item);
//            return await converter(item, scope);
//        }

//        private Task<Identifier[]> ConverterItemsAsReadOnlyEntryToIdentifiers(object item, ExecutionScope scope)
//        {
//            return Task.FromResult(new Identifier[] { ((IReadOnlyEntry)item).Id });
//        }

//        private Task<Identifier[]> ConverterItemsAsNodeToIdentifiers(object item, ExecutionScope scope)
//        {
//            return Task.FromResult(new Identifier[] { ((INode)item).Id });
//        }

//        private Task<Identifier[]> ConverterItemsAsIdentifierToIdentifiers(object item, ExecutionScope scope)
//        {
//            return Task.FromResult(new Identifier[] { (Identifier)item });
//        }

//        private async Task<Identifier[]> ConverterRootToIdentifiers(object item, ExecutionScope scope)
//        {
//            var root = await _context.Logical.Roots.Get(DefaultRoot.Tail);
//            return new Identifier[] { root.Identifier };
//        }
//    }
//}
