// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;

    public static class LogicalOptionsUseDataConnectionToNewSpaceExtension
    {
        public static async Task<LogicalOptions> UseDataConnectionToNewSpace(this LogicalOptions options, ILogicalTestContext testContext, bool openOnCreation)
        {
            var connection = await testContext.Fabric.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);

            var fabricOptions = new FabricContextOptions(testContext.ClientConfiguration)
                .Use(connection)
                .UseDiagnostics();
            var fabricContext = new FabricContextFactory().Create(fabricOptions);

            options.UseFabricContext(fabricContext);
            return options;
        }

        public static async Task<LogicalOptions> UseDataConnectionToNewSpace(this LogicalOptions options, LogicalUnitTestContext unitTestContext, bool openOnCreation)
        {
            var connection = await unitTestContext.Fabric.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);

            var fabricOptions = new FabricContextOptions(unitTestContext.ClientConfiguration)
                .Use(connection)
                .UseDiagnostics();
            var fabricContext = new FabricContextFactory().Create(fabricOptions);

            options.UseFabricContext(fabricContext);
            return options;
        }
    }
}
