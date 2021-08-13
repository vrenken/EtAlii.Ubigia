// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Context
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class AntlrGraphContextExtension : IFunctionalExtension
    {
        private readonly FunctionalOptions _options;

        public AntlrGraphContextExtension(FunctionalOptions options)
        {
            _options = options;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            container.Register<IFunctionalOptions>(() => _options);
            container.Register<ISchemaProcessorFactory, AntlrSchemaProcessorFactory>();
            container.Register<ISchemaParserFactory, AntlrSchemaParserFactory>();
            container.Register(() => new LogicalContextFactory().Create(_options));
        }
    }
}
