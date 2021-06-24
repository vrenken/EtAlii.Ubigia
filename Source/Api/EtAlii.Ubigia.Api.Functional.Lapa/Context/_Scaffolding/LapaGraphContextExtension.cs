// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaGraphContextExtension : IGraphContextExtension
    {
        private readonly FunctionalContextConfiguration _configuration;

        public LapaGraphContextExtension(FunctionalContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize(Container container)
        {
            container.Register<IGraphContext, GraphContext>();
            container.Register<IFunctionalContextConfiguration>(() => _configuration);

            container.Register<ISchemaProcessorFactory, LapaSchemaProcessorFactory>();
            container.Register<ISchemaParserFactory, LapaSchemaParserFactory>();

            container.Register(() => new TraversalContextFactory().Create(_configuration));

            container.Register(() => new LogicalContextFactory().Create(_configuration));
        }
    }
}
