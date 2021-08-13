// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Microsoft.Extensions.Configuration;

    internal class TestScriptParserFactory : ScriptParserFactory
    {
        public IScriptParser Create(IConfigurationRoot configurationRoot)
        {
            var options = new FunctionalOptions(configurationRoot)
                .UseTestParser()
                .UseFunctionalDiagnostics();

            return Create(options);
        }
    }
}
