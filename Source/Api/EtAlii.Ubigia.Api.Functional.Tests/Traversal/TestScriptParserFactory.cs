// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Microsoft.Extensions.Configuration;

    internal class TestScriptParserFactory : ScriptParserFactory
    {
        public IScriptParser Create(IConfiguration configurationRoot)
        {
            var configuration = new TraversalParserConfiguration()
                .UseFunctionalDiagnostics(configurationRoot)
                .UseTestParser();

            return Create(configuration);
        }
    }
}
