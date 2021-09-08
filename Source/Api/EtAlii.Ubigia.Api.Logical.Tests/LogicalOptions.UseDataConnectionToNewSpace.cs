// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public static class FabricOptionsUseDataConnectionToNewSpaceExtension
    {
        public static async Task<FabricOptions> UseDataConnectionToNewSpace(this FabricOptions options, ILogicalTestContext testContext, bool openOnCreation)
        {
            var connection = await testContext.Fabric.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);

            return options.Use(connection);
        }

        public static async Task<FabricOptions> UseDataConnectionToNewSpace(this FabricOptions options, LogicalUnitTestContext unitTestContext, bool openOnCreation)
        {
            var connection = await unitTestContext.Fabric.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);

            return options.Use(connection);
        }
    }
}
