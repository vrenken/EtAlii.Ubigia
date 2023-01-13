// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using EtAlii.Ubigia.Api.Fabric;
using EtAlii.xTechnology.MicroContainer;
using HashLib;

public class ContentManagerFactory : IContentManagerFactory
{
    internal ContentManagerFactory()
    {
    }

    public IContentManager Create(IFabricContext fabric)
    {
        var container = new Container();

        container.Register(() => fabric);
        container.Register<IContentManager, ContentManager>();
        container.Register(HashFactory.Checksum.CreateCRC64_ECMA);
        container.Register<IContentPartStoreCommandHandler, ContentPartStoreCommandHandler>();
        container.Register<IContentPartQueryHandler, ContentPartQueryHandler>();
        container.Register<IContentDefinitionQueryHandler, ContentDefinitionQueryHandler>();
        container.Register<IContentQueryHandler, ContentQueryHandler>();
        container.Register<IContentNewQueryHandler, ContentNewQueryHandler>();
        container.Register<IContentPartCalculator, ContentPartCalculator>();

        return container.GetInstance<IContentManager>();
    }
}
