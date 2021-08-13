// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Context
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.xTechnology.MicroContainer;

    public class AntlrSchemaParserFactory : Factory<ISchemaParser, FunctionalOptions, IFunctionalExtension>, ISchemaParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(FunctionalOptions options)
        {
            return Array.Empty<IScaffolding>();
        }
    }
}
