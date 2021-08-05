// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    public class FabricContextFactory : Factory<IFabricContext, FabricContextOptions, IFabricContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(FabricContextOptions options)
        {
            return new IScaffolding[]
            {
                new ContextScaffolding(options),
                new EntryContextScaffolding(options.TraversalCachingEnabled),
                new ContentContextScaffolding(options.TraversalCachingEnabled),
                new PropertyContextScaffolding(options.TraversalCachingEnabled),
                new RootContextScaffolding(),
            };
        }
    }
}
