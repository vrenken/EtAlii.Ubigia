// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.xTechnology.MicroContainer;

    internal class GraphContextScaffolding : IScaffolding
    {
        private readonly FunctionalOptions _options;

        public GraphContextScaffolding(FunctionalOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IGraphContext>(services =>
            {
                var schemaProcessorFactory = services.GetInstance<ISchemaProcessorFactory>();
                var schemaParserFactory = services.GetInstance<ISchemaParserFactory>();
                return new GraphContext(_options, schemaProcessorFactory, schemaParserFactory);
            });
        }
    }
}
