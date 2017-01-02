namespace EtAlii.Ubigia.Api.Logical
{
    using System.Linq;
    using System.Threading.Tasks;

    internal class PropertiesToIdentifierAssigner : IPropertiesToIdentifierAssigner
    {
        private readonly IAssignmentContext _context;
        private readonly IUpdateEntryFactory _updateEntryFactory;

        public PropertiesToIdentifierAssigner(
            IAssignmentContext context, 
            IUpdateEntryFactory updateEntryFactory)
        {
            _context = context;
            _updateEntryFactory = updateEntryFactory;
        }

        public async Task<INode> Assign(object o, Identifier id, ExecutionScope scope)
        {
            IPropertyDictionary newProperties = (IPropertyDictionary) o;
            var entry = await _context.Fabric.Entries.Get(id, scope);
            var oldProperties = await _context.Fabric.Properties.Retrieve(entry.Id, scope) ?? new PropertyDictionary();

            var nodeShouldBeUpdated = ShouldUpdateNode(oldProperties, newProperties);
            if (!nodeShouldBeUpdated)
            {
                // The two propertydictionaries are the same. Let's return the old node.
                return (IInternalNode)new DynamicNode((IReadOnlyEntry)entry, oldProperties);
            }
            else
            {

                // We want to do an addition, so lets combine the old with the new properties.
                var properties = new PropertyDictionary(oldProperties);

                foreach (var newProperty in newProperties)
                {
                    if (newProperty.Value == null)
                    {
                        // Lets remove all properties that should be set to null.
                        properties.Remove(newProperty.Key);
                    }
                    else
                    {
                        // And update the rest.
                        properties[newProperty.Key] = newProperty.Value;
                    }
                }

                // Only update if we something changed - no need to update them when nothing changed.
                if (!properties.Equals(oldProperties))
                {
                    var newEntry = await _updateEntryFactory.Create(entry, scope);

                    await _context.Fabric.Properties.Store(newEntry.Id, properties, scope);

                    var newNode = (IInternalNode) new DynamicNode((IReadOnlyEntry) newEntry, properties);
                    return newNode;
                }
                else
                {
                    return (IInternalNode)new DynamicNode((IReadOnlyEntry)entry, properties);
                }
            }
        }

        /// <summary>
        /// We need to find out if the property dictionaries differ and the node should be updated.
        /// </summary>
        /// <param name="oldPropertyDictionary"></param>
        /// <param name="newPropertyDictionary"></param>
        /// <returns></returns>
        private bool ShouldUpdateNode(PropertyDictionary oldPropertyDictionary, IPropertyDictionary newPropertyDictionary)
        {
            if (newPropertyDictionary.Count > oldPropertyDictionary.Count)
            {
                // If the new property count is bigger as the old property count then always update.
                return true;
            }
            else if (newPropertyDictionary.Count == oldPropertyDictionary.Count)
            {
                // If the property count is the same then we completely need to check the two property dictionaries.
                //return newPropertyDictionary.CompareTo(oldPropertyDictionary) != 0;
                return !newPropertyDictionary.Equals(oldPropertyDictionary);
            }
            else
            {
                // If the new property count is smaller then the old property count then
                // we need to check only the properties that will be update.
                var intersectedProperties = newPropertyDictionary.Keys
                    .Intersect(oldPropertyDictionary.Keys)
                    .ToArray();
                var hasNewProperties = newPropertyDictionary.Keys
                    .Except(intersectedProperties)
                    .Any();
                if (hasNewProperties)
                {
                    return true;
                }

                foreach (var intersectedProperty in intersectedProperties)
                {
                    var oldValue = oldPropertyDictionary[intersectedProperty];
                    var newValue = oldPropertyDictionary[intersectedProperty];
                    if (oldValue != newValue)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}