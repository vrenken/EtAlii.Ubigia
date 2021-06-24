// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public class FabricContext : IFabricContext
    {
        public IItemsSet Items { get; }

        public IContentSet Content { get; }

        public IContentDefinitionSet ContentDefinition { get; }

        public IEntrySet Entries { get; }

        public IRootSet Roots { get; }

        public IPropertiesSet Properties { get; }

        public IIdentifierSet Identifiers { get; }

        public FabricContext(
            IItemsSet items, 
            IContentSet content, 
            IContentDefinitionSet contentDefinition,
            IEntrySet entries,
            IRootSet roots, 
            IPropertiesSet properties, 
            IIdentifierSet identifiers)
        {
            Items = items;
            Content = content;
            ContentDefinition = contentDefinition;
            Roots = roots;
            Properties = properties;
            Identifiers = identifiers;
            Entries = entries;
        }

        public void Start()
        {
            // Not needed (yet).
        }

        public void Stop()
        {
            // Not needed (yet).
        }
    }
}