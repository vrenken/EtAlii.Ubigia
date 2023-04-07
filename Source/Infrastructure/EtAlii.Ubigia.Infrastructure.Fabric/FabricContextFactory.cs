// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using EtAlii.xTechnology.MicroContainer;

public class FabricContextFactory : Factory<IFabricContext, FabricContextOptions>
{
    /// <inheritdoc />
    protected override IScaffolding[] CreateScaffoldings(FabricContextOptions options)
    {
        if (options.Storage == null)
        {
            throw new NotSupportedException("A Storage is required to construct a FabricContext instance");
        }

        return new IScaffolding[]
        {
            new FabricContextScaffolding(options),
        };
    }
}
