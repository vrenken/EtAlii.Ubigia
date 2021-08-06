// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    internal class GraphScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IGraphPathBuilder, GraphPathBuilder>();
            container.Register<IGraphComposerFactory, GraphComposerFactory>();
            container.Register<IGraphAssignerFactory, GraphAssignerFactory>();

            container.Register<IGraphPathTraverserFactory, GraphPathTraverserFactory>();
            container.Register(() =>
            {
                var fabric = container.GetInstance<IFabricContext>();
                var configurationRoot = container.GetInstance<IConfigurationRoot>();

                var options = new GraphPathTraverserOptions(configurationRoot)
                    .Use(fabric);
                var graphPathTraverserFactory = container.GetInstance<IGraphPathTraverserFactory>();
                return graphPathTraverserFactory.Create(options);
            });

        }
    }
}
