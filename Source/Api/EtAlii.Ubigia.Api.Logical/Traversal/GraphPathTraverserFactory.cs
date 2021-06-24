// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    public class GraphPathTraverserFactory : Factory<IGraphPathTraverser, GraphPathTraverserConfiguration, IGraphPathTraverserExtension>, IGraphPathTraverserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(GraphPathTraverserConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new TraversalScaffolding(configuration),
            };
        }
    }
}
