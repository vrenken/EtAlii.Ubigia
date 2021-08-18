// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;

    public static class FunctionalUnitTestContextExtensions
    {
        public static async Task<(TFirstInstance, TSecondInstance)> CreateComponentOnNewSpace<TFirstInstance, TSecondInstance>(this FunctionalUnitTestContext context)
        {
            var options = await new FunctionalOptions(context.ClientConfiguration)
                .UseTestParsing()
                .UseDataConnectionToNewSpace(context, true)
                .ConfigureAwait(false);

            return Factory.Create<TFirstInstance, TSecondInstance, IFunctionalExtension>(options);
        }

        public static async Task<TInstance> CreateComponentOnNewSpace<TInstance>(this FunctionalUnitTestContext context)
        {
            var options = await new FunctionalOptions(context.ClientConfiguration)
                .UseTestParsing()
                .UseDataConnectionToNewSpace(context, true)
                .ConfigureAwait(false);

            return Factory.Create<TInstance, IFunctionalExtension>(options);
        }

        public static ISchemaProcessor CreateSchemaProcessor(this FunctionalUnitTestContext context, IDataConnection dataConnection)
        {
            var options = new FunctionalOptions(context.ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics()
                .Use(dataConnection);

            return Factory.Create<ISchemaProcessor, IFunctionalExtension>(options);
        }

        public static IScriptParser CreateScriptParser(this FunctionalUnitTestContext context)
        {
            var options = new FunctionalOptions(context.ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics();

            return Factory.Create<IScriptParser, IFunctionalExtension>(options);
        }

        public static IScriptProcessor CreateScriptProcessor(this FunctionalUnitTestContext context, ILogicalContext logicalContext)
        {
            var options = new FunctionalOptions(context.ClientConfiguration)
                .UseTestParsing()
                .Use(logicalContext.Options.Connection)
                .UseFunctionalDiagnostics();

            return Factory.Create<IScriptProcessor, IFunctionalExtension>(options);
        }

        public static async Task<IScriptProcessor> CreateScriptProcessorOnNewSpace(this FunctionalUnitTestContext context)
        {
            var options = await new FunctionalOptions(context.ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics()
                .UseDataConnectionToNewSpace(context, true)
                .ConfigureAwait(false);

            return Factory.Create<IScriptProcessor, IFunctionalExtension>(options);
        }

        public static ISchemaParser CreateSchemaParser(this FunctionalUnitTestContext context)
        {
            var options = new FunctionalOptions(context.ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics();

            return Factory.Create<ISchemaParser, IFunctionalExtension>(options);
        }
    }
}
