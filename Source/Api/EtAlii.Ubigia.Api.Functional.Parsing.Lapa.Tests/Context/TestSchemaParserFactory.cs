// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
    using Microsoft.Extensions.Configuration;

    internal class TestSchemaParserFactory : LapaSchemaParserFactory
    {
        public ISchemaParser Create()
        {
            var configurationRoot = new ConfigurationBuilder()
                .Build();

            var options = new FunctionalOptions(configurationRoot)
                .UseLapaParsing();
            return base.Create(options);
        }
    }
}
