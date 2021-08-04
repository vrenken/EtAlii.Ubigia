// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Context
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class AntlrSchemaParserScaffolding : IScaffolding
    {
        private readonly ParserOptions _parserOptions;

        public AntlrSchemaParserScaffolding(ParserOptions parserOptions)
        {
            _parserOptions = parserOptions;
        }

        public void Register(Container container)
        {
            container.Register<IContextValidator, ContextValidator>();
            container.Register<ISchemaParser, AntlrSchemaParser>();
            container.Register(() => new PathParserFactory().Create(_parserOptions));
        }
    }
}
