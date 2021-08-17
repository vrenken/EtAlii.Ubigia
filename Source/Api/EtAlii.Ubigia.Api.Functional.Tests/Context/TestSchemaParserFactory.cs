// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
#if USE_LAPA_PARSING_IN_TESTS
    internal class TestSchemaParserFactory : LapaSchemaParserFactory
    {
        public ISchemaParser Create()
        {
            var options = new FunctionalContextOptions().UseLapaParsing();
            return base.Create(options);
        }
    }
#else
    using EtAlii.Ubigia.Api.Functional.Antlr;
    using EtAlii.Ubigia.Api.Functional.Antlr.Context;
    using Microsoft.Extensions.Configuration;

    internal class TestSchemaParserFactory : AntlrSchemaParserFactory
    {
        public ISchemaParser Create()
        {
            var configurationRoot = new ConfigurationBuilder()
                .Build();

            var options = new FunctionalOptions(configurationRoot)
                .UseAntlrParsing();
            return base.Create(options);
        }
    }
#endif
}
