// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public static class LogicalOptionsUseDataConnectionToNewSpaceExtension
    {
        public static async Task<TLogicalOptions> UseDataConnectionToNewSpace<TLogicalOptions>(this TLogicalOptions options, ILogicalTestContext testContext, bool openOnCreation)
            where TLogicalOptions : LogicalOptions, ILogicalOptions
        {
            var connection = await testContext.Fabric.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);
            options.Use(connection);
            return options;
        }

        public static async Task<TLogicalOptions> UseDataConnectionToNewSpace<TLogicalOptions>(this TLogicalOptions options, LogicalUnitTestContext unitTestContext, bool openOnCreation)
            where TLogicalOptions : LogicalOptions, ILogicalOptions
        {
            var connection = await unitTestContext.Fabric.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);
            options.Use(connection);
            return options;
        }
    }
}
