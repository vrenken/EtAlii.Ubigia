// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class FabricContextFactory : Factory<IFabricContext, FabricContextConfiguration, IFabricContextExtension>// IFabricContextFactory
    {
        protected override IScaffolding[] CreateScaffoldings(FabricContextConfiguration configuration)
        {
            if (configuration.Storage == null)
            {
                throw new NotSupportedException("A Storage is required to construct a FabricContext instance");
            }

            return new IScaffolding[]
            {
                new FabricContextScaffolding(configuration.Storage),
                new ItemsScaffolding(),
                new IdentifiersScaffolding(), 
                new ContentScaffolding(),
                new ContentDefinitionScaffolding(),
                new RootsScaffolding(),
                new PropertiesScaffolding(),
                new EntryScaffolding(),
            };
        }
    }
}