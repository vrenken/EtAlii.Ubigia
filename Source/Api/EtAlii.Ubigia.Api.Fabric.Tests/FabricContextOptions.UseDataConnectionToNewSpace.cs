// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System.Threading.Tasks;

    public static class FabricTestContextUseDataConnectionToNewSpaceExtension
    {
        public static async Task<FabricContextOptions> UseDataConnectionToNewSpace(this FabricContextOptions options, FabricUnitTestContext unitTestContext, bool openOnCreation)
        {
            var connection = await unitTestContext.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);
            options.Use(connection);
            return options;
        }

        public static async Task<FabricContextOptions> UseDataConnectionToNewSpace(this FabricContextOptions options, IFabricTestContext testContext, bool openOnCreation)
        {
            var connection = await testContext.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);
            options.Use(connection);
            return options;
        }
    }
}
