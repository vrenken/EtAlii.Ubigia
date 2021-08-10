// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    internal class GraphScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IGraphPathBuilder, GraphPathBuilder>();
            container.Register<IGraphComposerFactory, GraphComposerFactory>();
            container.Register<IGraphAssignerFactory, GraphAssignerFactory>();

            container.Register<IGraphPathTraverserFactory, GraphPathTraverserFactory>();
            container.Register(services =>
            {
                var fabric = services.GetInstance<IFabricContext>();
                var configurationRoot = services.GetInstance<IConfigurationRoot>();

                var options = new GraphPathTraverserOptions(configurationRoot)
                    .Use(fabric);
                var graphPathTraverserFactory = services.GetInstance<IGraphPathTraverserFactory>();
                return graphPathTraverserFactory.Create(options);
            });

        }
    }
}
