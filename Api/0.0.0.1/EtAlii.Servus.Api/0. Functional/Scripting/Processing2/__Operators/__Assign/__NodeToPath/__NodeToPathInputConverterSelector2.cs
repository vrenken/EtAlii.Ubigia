//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Threading.Tasks;
//    using EtAlii.Servus.Api.Logical;
//    using EtAlii.xTechnology.Structure;

//    // TODO: Merge with DynamicObjectToPathInputConverterSelector. These classes are duplicates.

//    internal class NodeToPathInputConverterSelector2 : Selector<object, Func<object, ExecutionScope, Task<IReadOnlyEntry>>>, INodeToPathInputConverterSelector2 
//    {
//        private readonly IProcessingContext _context;

//        public NodeToPathInputConverterSelector2(
//            IProcessingContext context)
//        {
//            _context = context;
//            Register(item => item is Identifier, (item, scope) => ConvertIdentifierToReadOnlyEntry((Identifier)item, scope))
//                .Register(item => item is IReadOnlyEntry, (item, scope) => Task.FromResult((IReadOnlyEntry)item))
//                .Register(item => item is INode, (item, scope) => ConvertNodeToReadOnlyEntry((INode)item, scope));
//        }

//        private async Task<IReadOnlyEntry> ConvertNodeToReadOnlyEntry(INode node, ExecutionScope scope)
//        {
//            return await ConvertIdentifierToReadOnlyEntry(node.Id, scope);
//        }
//        private async Task<IReadOnlyEntry> ConvertIdentifierToReadOnlyEntry(Identifier identifier, ExecutionScope scope)
//        {
//            return await _context.Logical.Nodes.Select(GraphPath.Create(identifier), scope);
//        }
//    }
//}
