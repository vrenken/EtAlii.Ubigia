// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Hosting;

    internal class TestScriptParserFactory : ScriptParserFactory
    {
        public IScriptParser Create()
        {
            var configuration = new TraversalParserConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .UseTestParser();

            return Create(configuration);
        }
    }
}
