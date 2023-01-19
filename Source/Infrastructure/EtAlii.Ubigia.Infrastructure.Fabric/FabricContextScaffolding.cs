// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using EtAlii.xTechnology.MicroContainer;
using HashLib;

internal class FabricContextScaffolding : IScaffolding
{
    private readonly FabricContextOptions _options;

    public FabricContextScaffolding(FabricContextOptions options)
    {
        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IFabricContext, FabricContext>();
        container.Register(() => _options.Storage);
        container.Register(() => _options.ConfigurationRoot);

        // Identifiers
        container.Register<IIdentifierSet, IdentifierSet>();

        // Items
        container.Register<IItemsSet, ItemsSet>();
        container.Register<IItemAdder, ItemAdder>();
        container.Register<IItemRemover, ItemRemover>();
        container.Register<IItemGetter, ItemGetter>();
        container.Register<IItemUpdater, ItemUpdater>();

        // Entries
        container.Register<IEntrySet, EntrySet>();
        container.Register<IEntryUpdater, EntryUpdater>();
        container.Register<IEntryGetter, EntryGetter>();
        container.Register<IEntryStorer, EntryStorer>();

        // Properties
        container.Register<IPropertiesSet, PropertiesSet>();
        container.Register<IPropertiesGetter, PropertiesGetter>();
        container.Register<IPropertiesStorer, PropertiesStorer>();

        // Roots
        container.Register<IRootSet, RootSet>();
        container.Register<IRootGetter, RootGetter>();
        container.Register<IRootAdder, RootAdder>();
        container.Register<IRootRemover, RootRemover>();
        container.Register<IRootUpdater, RootUpdater>();

        // Content
        container.Register<IContentSet, ContentSet>();
        container.Register<IContentGetter, ContentGetter>();
        container.Register<IContentPartGetter, ContentPartGetter>();
        container.Register<IContentStorer, ContentStorer>();
        container.Register<IContentPartStorer, ContentPartStorer>();
        container.Register(HashFactory.Checksum.CreateCRC64_ECMA);

        // ContentDefinition
        container.Register<IContentDefinitionSet, ContentDefinitionSet>();
        //container.Register<IContentDefinitionRepository, ContentDefinitionRepository>()
        container.Register<IContentDefinitionGetter, ContentDefinitionGetter>();
        container.Register<IContentDefinitionPartGetter, ContentDefinitionPartGetter>();
        container.Register<IContentDefinitionStorer, ContentDefinitionStorer>();
        container.Register<IContentDefinitionPartStorer, ContentDefinitionPartStorer>();
    }
}
