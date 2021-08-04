// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Context
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.xTechnology.MicroContainer;

    public class AntlrSchemaParserFactory : Factory<ISchemaParser, SchemaParserOptions, ISchemaParserExtension>, ISchemaParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(SchemaParserOptions options)
        {
            return new IScaffolding[]
            {
                new AntlrSchemaParserScaffolding(options.TraversalParserOptions),
            };
        }
    }
}
