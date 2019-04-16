namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal class ConstantToIdentifierTagAssigner : IConstantToIdentifierTagAssigner
    {
        private readonly IFabricContext _fabric;
        private readonly IGraphPathTraverser _graphPathTraverser;
        private readonly IUpdateEntryFactory _updateEntryFactory;

        public ConstantToIdentifierTagAssigner(
            IUpdateEntryFactory updateEntryFactory, 
            IFabricContext fabric,
            IGraphPathTraverser graphPathTraverser)
        {
            _updateEntryFactory = updateEntryFactory;
            _fabric = fabric;
            _graphPathTraverser = graphPathTraverser;
        }

        public async Task<INode> Assign(string tag, Identifier id, ExecutionScope scope)
        {
            var latestEntry = await _graphPathTraverser.TraverseToSingle(id, scope);
            id = latestEntry.Id;

            var entry = await _fabric.Entries.Get(id, scope);
            var updatedEntry = await _updateEntryFactory.Create(entry, tag, scope);

            var newNode = (IInternalNode)new DynamicNode((IReadOnlyEntry)updatedEntry);
            return newNode;
        }
    }
}