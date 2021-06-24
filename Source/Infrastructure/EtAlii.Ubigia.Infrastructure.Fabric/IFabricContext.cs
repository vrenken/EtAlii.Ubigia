// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IFabricContext
    {
        IItemsSet Items { get; }

        IContentSet Content { get; }
        IContentDefinitionSet ContentDefinition { get; }

        IEntrySet Entries { get; }

        IRootSet Roots { get; }

        IPropertiesSet Properties { get; }

        IIdentifierSet Identifiers { get; }

        void Start();
        void Stop();
    }
}