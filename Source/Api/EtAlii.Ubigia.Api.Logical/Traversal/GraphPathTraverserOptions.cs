// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using Microsoft.Extensions.Configuration;

    public class GraphPathTraverserOptions : ConfigurationBase, IGraphPathTraverserOptions
    {
        /// <inheritdoc />
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc />
        public IFabricContext FabricContext { get; private set; }

        public GraphPathTraverserOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }

        /// <inheritdoc />
        public GraphPathTraverserOptions Use(IFabricContext fabricContext)
        {
            FabricContext = fabricContext;
            return this;
        }
    }
}
