// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Context
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.xTechnology.MicroContainer;

    public class AntlrSchemaParserFactory : Factory<ISchemaParser, FunctionalOptions, ISchemaParserExtension>, ISchemaParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(FunctionalOptions options)
        {
            return new IScaffolding[]
            {
                new AntlrSchemaParserScaffolding(),
            };
        }
    }
}
