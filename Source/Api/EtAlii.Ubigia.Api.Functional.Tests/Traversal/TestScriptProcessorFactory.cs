// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Hosting;

    internal class TestScriptProcessorFactory : ScriptProcessorFactory
    {
        public IScriptProcessor Create(ILogicalContext logicalContext, ScriptScope scope = null)
        {
            scope ??= new ScriptScope();
            var configuration = new TraversalProcessorConfiguration()
                .UseFunctionalDiagnostics(TestClientConfiguration.Root)
                .UseTestProcessor()
                .Use(logicalContext)
                .Use(scope);
            return Create(configuration);
        }
    }
}
