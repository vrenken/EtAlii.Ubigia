// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using Microsoft.Extensions.Configuration;

    public interface IGraphPathTraverserConfiguration : IExtensible
    {
        IConfiguration ConfigurationRoot { get; }
        IFabricContext FabricContext { get; }

        GraphPathTraverserConfiguration Use(IFabricContext fabricContext);
    }
}
