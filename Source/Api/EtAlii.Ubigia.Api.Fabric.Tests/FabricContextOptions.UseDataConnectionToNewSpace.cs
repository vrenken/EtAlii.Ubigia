// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System.Threading.Tasks;

    public static class FabricTestContextUseDataConnectionToNewSpaceExtension
    {
        public static async Task<FabricOptions> UseDataConnectionToNewSpace(this FabricOptions options, FabricUnitTestContext unitTestContext, bool openOnCreation)
        {
            var (connection, _) = await unitTestContext.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);
            options.Use(connection);
            return options;
        }

        public static async Task<FabricOptions> UseDataConnectionToNewSpace(this FabricOptions options, IFabricTestContext testContext, bool openOnCreation)
        {
            var (connection, _) = await testContext.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);
            options.Use(connection);
            return options;
        }
    }
}
