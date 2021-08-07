// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaGraphContextExtension : IGraphContextExtension
    {
        private readonly FunctionalOptions _options;

        public LapaGraphContextExtension(FunctionalOptions options)
        {
            _options = options;
        }

        public void Initialize(Container container)
        {
            container.Register<IFunctionalOptions>(() => _options);

            container.Register<ISchemaProcessorFactory, LapaSchemaProcessorFactory>();
            container.Register<ISchemaParserFactory, LapaSchemaParserFactory>();

            container.Register(() => new TraversalContextFactory().Create(_options));

            container.Register(() => new LogicalContextFactory().Create(_options));
        }
    }
}
