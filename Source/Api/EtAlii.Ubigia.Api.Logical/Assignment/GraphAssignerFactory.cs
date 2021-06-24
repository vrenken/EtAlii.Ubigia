// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphAssignerFactory : IGraphAssignerFactory
    {
        private readonly IGraphPathTraverser _graphPathTraverser;

        public GraphAssignerFactory(IGraphPathTraverser graphPathTraverser)
        {
            _graphPathTraverser = graphPathTraverser;
        }

        public IGraphAssigner Create(IFabricContext fabric)
        {
            var container = new Container();

            container.Register(() => fabric);
            container.Register<IGraphAssigner, GraphAssigner>();
            container.Register(() => _graphPathTraverser);

            // Helpers:
            container.Register<IUpdateEntryFactory, UpdateEntryFactory>();

            container.Register<IPropertiesToIdentifierAssigner, PropertiesToIdentifierAssigner>();
            container.Register<IDynamicObjectToIdentifierAssigner, DynamicObjectToIdentifierAssigner>();
            container.Register<INodeToIdentifierAssigner, NodeToIdentifierAssigner>();
            container.Register<IConstantToIdentifierTagAssigner, ConstantToIdentifierTagAssigner>();

            return container.GetInstance<IGraphAssigner>();

        }
    }
}