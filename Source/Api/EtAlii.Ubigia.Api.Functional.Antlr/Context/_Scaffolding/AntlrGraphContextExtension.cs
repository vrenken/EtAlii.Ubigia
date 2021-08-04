// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Context
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class AntlrGraphContextExtension : IGraphContextExtension
    {
        private readonly FunctionalContextOptions _options;

        public AntlrGraphContextExtension(FunctionalContextOptions options)
        {
            _options = options;
        }

        public void Initialize(Container container)
        {
            container.Register<IFunctionalContextOptions>(() => _options);

            container.Register<ISchemaProcessorFactory, AntlrSchemaProcessorFactory>();
            container.Register<ISchemaParserFactory, AntlrSchemaParserFactory>();

            container.Register(() => new TraversalContextFactory().Create(_options));

            container.Register(() => new LogicalContextFactory().Create(_options));
        }
    }
}
