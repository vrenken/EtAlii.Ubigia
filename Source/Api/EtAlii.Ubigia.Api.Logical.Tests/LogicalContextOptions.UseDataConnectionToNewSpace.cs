// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public static class LogicalTestContextUseDataConnectionToNewSpaceExtension
    {
        public static async Task<TLogicalContextOptions> UseDataConnectionToNewSpace<TLogicalContextOptions>(this TLogicalContextOptions options, ILogicalTestContext testContext, bool openOnCreation)
            where TLogicalContextOptions : LogicalContextOptions, ILogicalContextOptions
        {
            var connection = await testContext.Fabric.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);
            options.Use(connection);
            return options;
        }

        public static async Task<TLogicalContextOptions> UseDataConnectionToNewSpace<TLogicalContextOptions>(this TLogicalContextOptions options, LogicalUnitTestContext unitTestContext, bool openOnCreation)
            where TLogicalContextOptions : LogicalContextOptions, ILogicalContextOptions
        {
            var connection = await unitTestContext.Fabric.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);
            options.Use(connection);
            return options;
        }
    }
}
