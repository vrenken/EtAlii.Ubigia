// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal class PropertiesToIdentifierAssigner : IPropertiesToIdentifierAssigner
    {
        private readonly IUpdateEntryFactory _updateEntryFactory;
        private readonly IFabricContext _fabric;
        private readonly IGraphPathTraverser _graphPathTraverser;

        public PropertiesToIdentifierAssigner(
            IUpdateEntryFactory updateEntryFactory,
            IFabricContext fabric,
            IGraphPathTraverser graphPathTraverser)
        {
            _updateEntryFactory = updateEntryFactory;
            _fabric = fabric;
            _graphPathTraverser = graphPathTraverser;
        }

        public async Task<IReadOnlyEntry> Assign(IPropertyDictionary properties, Identifier id, ExecutionScope scope)
        {
            var latestEntry = await _graphPathTraverser
                .TraverseToSingle(id, scope)
                .ConfigureAwait(false);
            id = latestEntry.Id;

            var entry = await _fabric.Entries
                .Get(id, scope)
                .ConfigureAwait(false);
            var oldProperties = await _fabric.Properties
                .Retrieve(entry.Id, scope)
                .ConfigureAwait(false) ?? new PropertyDictionary();

            var nodeShouldBeUpdated = ShouldUpdateNode(oldProperties, properties);
            if (!nodeShouldBeUpdated)
            {
                // The two PropertyDictionaries are the same. Let's return the old entry.
                return entry;
            }
            else
            {

                // We want to do an addition, so lets combine the old with the new properties.
                var newProperties = new PropertyDictionary(oldProperties);

                foreach (var newProperty in properties)
                {
                    if (newProperty.Value == null)
                    {
                        // Lets remove all properties that should be set to null.
                        newProperties .Remove(newProperty.Key);
                    }
                    else
                    {
                        // And update the rest.
                        newProperties [newProperty.Key] = newProperty.Value;
                    }
                }

                // Only update if we something changed - no need to update them when nothing changed.
                if (!newProperties .Equals(oldProperties))
                {
                    var newEntry = await _updateEntryFactory
                        .Create(entry, scope)
                        .ConfigureAwait(false);

                    await _fabric.Properties
                        .Store(newEntry.Id, newProperties , scope)
                        .ConfigureAwait(false);

                    return (IReadOnlyEntry)newEntry;
                }
                else
                {
                    return entry;
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
                //return newPropertyDictionary.CompareTo(oldPropertyDictionary) != 0
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
