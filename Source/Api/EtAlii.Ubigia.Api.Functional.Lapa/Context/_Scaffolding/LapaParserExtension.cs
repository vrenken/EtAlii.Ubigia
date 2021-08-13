// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaParserExtension : IFunctionalExtension
    {
        private readonly FunctionalOptions _options;

        public LapaParserExtension(FunctionalOptions options)
        {
            _options = options;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            container.Register<IFunctionalOptions>(() => _options);

            new LapaSchemaParserScaffolding().Register(container);
            new LapaScriptParserScaffolding().Register(container);

            new LapaSequenceParsingScaffolding().Register(container);
            new LapaSubjectParsingScaffolding().Register(container);
            new LapaOperatorParsingScaffolding().Register(container);
            new LapaPathSubjectParsingScaffolding().Register(container);
            new LapaConstantParsingScaffolding().Register(container);

            container.Register<ISchemaProcessorFactory, LapaSchemaProcessorFactory>();
            container.Register<ISchemaParserFactory, LapaSchemaParserFactory>();

            container.Register(() => new LogicalContextFactory().Create(_options));
        }
    }
}
