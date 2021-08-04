// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class GraphContextScaffolding : IScaffolding
    {
        private readonly FunctionalContextOptions _options;

        public GraphContextScaffolding(FunctionalContextOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            container.Register(() => _options.ConfigurationRoot);

            container.Register<IGraphContext>(() =>
            {
                var schemaProcessorFactory = container.GetInstance<ISchemaProcessorFactory>();
                var schemaParserFactory = container.GetInstance<ISchemaParserFactory>();
                var traversalContext = container.GetInstance<ITraversalContext>();
                return new GraphContext(_options.ParserOptions, schemaProcessorFactory, schemaParserFactory, traversalContext);
            });
        }
    }
}
