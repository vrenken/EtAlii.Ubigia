// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    public static class FunctionalUnitTestContextExtensions
    {
        public static async Task<(TFirstInstance, TSecondInstance)> CreateComponentOnNewSpace<TFirstInstance, TSecondInstance>(this FunctionalUnitTestContext context)
        {
            var options = await new FunctionalOptions(context.ClientConfiguration)
                .UseLapaParsing()
                .UseLogicalContext(context, true)
                .ConfigureAwait(false);

            return Factory.Create<TFirstInstance, TSecondInstance, IExtension>(options);
        }

        public static async Task<TInstance> CreateComponentOnNewSpace<TInstance>(this FunctionalUnitTestContext context)
        {
            var options = await new FunctionalOptions(context.ClientConfiguration)
                .UseLapaParsing()
                .UseLogicalContext(context, true)
                .ConfigureAwait(false);

            return Factory.Create<TInstance, IExtension>(options);
        }

        public static IScriptParser CreateScriptParser(this FunctionalUnitTestContext context)
        {
            var options = new FunctionalOptions(context.ClientConfiguration)
                .UseLapaParsing()
                .UseFunctionalDiagnostics()
                .UseLogicalContext(context);

            return Factory.Create<IScriptParser, IExtension>(options);
        }

        public static ISchemaParser CreateSchemaParser(this FunctionalUnitTestContext context)
        {
            var options = new FunctionalOptions(context.ClientConfiguration)
                .UseLapaParsing()
                .UseFunctionalDiagnostics()
                .UseLogicalContext(context);

            return Factory.Create<ISchemaParser, IExtension>(options);
        }
    }
}
