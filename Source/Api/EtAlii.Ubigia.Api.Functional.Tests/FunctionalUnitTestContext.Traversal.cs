// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public partial class FunctionalUnitTestContext
    {
        public async Task<IScriptProcessor> CreateScriptProcessorOnNewSpace()
        {
            var options = await new FunctionalOptions(ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics()
                .UseDataConnectionToNewSpace(this, true)
                .ConfigureAwait(false);

            var container = new Container();

            foreach (var extension in options.GetExtensions<IFunctionalExtension>())
            {
                extension.Initialize(container);
            }

            return  container.GetInstance<IScriptProcessor>();
        }

        public IScriptProcessor CreateScriptProcessor(ILogicalContext logicalContext)
        {
            var options = new FunctionalOptions(ClientConfiguration)
                .UseTestParsing()
                .Use(logicalContext.Options.Connection)
                .UseFunctionalDiagnostics();

            var container = new Container();

            foreach (var extension in options.GetExtensions<IFunctionalExtension>())
            {
                extension.Initialize(container);
            }

            return  container.GetInstance<IScriptProcessor>();
        }

        public IScriptParser CreateScriptParser()
        {
            var options = new FunctionalOptions(ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics();

            var container = new Container();

            foreach (var extension in options.GetExtensions<IFunctionalExtension>())
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IScriptParser>();
        }
    }
}
