namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    internal class NodeToIdentifierAssigner : INodeToIdentifierAssigner
    {
        private readonly IAssignmentContext _context;
        private readonly IUpdateEntryFactory _updateEntryFactory;

        public NodeToIdentifierAssigner(IAssignmentContext context,
            IUpdateEntryFactory updateEntryFactory)
        {
            _context = context;
            _updateEntryFactory = updateEntryFactory;
        }

        public async Task<INode> Assign(object o, Identifier id, ExecutionScope scope)
        {
            IInternalNode sourceNode = (IInternalNode) o;
            var newProperties = sourceNode.GetProperties();

            var entry = await _context.Fabric.Entries.Get(id, scope);
            var oldProperties = await _context.Fabric.Properties.Retrieve(id, scope) ?? new PropertyDictionary();

            if (oldProperties.CompareTo(newProperties) == 0)
            {
                // The two propertydictionaries are the same. Let's return the old node.
                return new DynamicNode(entry, oldProperties);
            }
            else
            {
                var newEntry = await _updateEntryFactory.Create(entry, scope);

                // We want to do an addition, so lets combine the old with the new properties.
                var properties = new PropertyDictionary(oldProperties);

                // Lets remove all properties that should be set to null.
                foreach (var newProperty in newProperties)
                {
                    if (newProperty.Value == null)
                    {
                        properties.Remove(newProperty.Key);
                    }
                    else
                    {
                        properties[newProperty.Key] = newProperty.Value;
                    }
                }

                await _context.Fabric.Properties.Store(newEntry.Id, properties, scope);

                var newNode = (IInternalNode)new DynamicNode((IReadOnlyEntry)newEntry, properties);
                return newNode;
            }
        }
    }
}