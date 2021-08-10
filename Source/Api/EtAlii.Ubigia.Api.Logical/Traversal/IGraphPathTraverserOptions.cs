// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public interface IGraphPathTraverserOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root that will be used to configure the graph path traverser.
        /// </summary>
        IConfigurationRoot ConfigurationRoot { get; }

        IFabricContext FabricContext { get; }

        GraphPathTraverserOptions Use(IFabricContext fabricContext);
    }
}
