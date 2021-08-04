// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface IGraphPathTraverserConfiguration : IExtensible
    {
        IFabricContext FabricContext { get; }

        GraphPathTraverserConfiguration Use(IFabricContext fabricContext);
    }
}
