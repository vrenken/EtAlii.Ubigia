// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;

    public static class FunctionalTestContextUseTraversalDataConnectionToNewSpaceExtension
    {
        public static async Task<TFunctionalOptions> UseDataConnectionToNewSpace<TFunctionalOptions>(this TFunctionalOptions options, FunctionalUnitTestContext unitTestContext, bool openOnCreation)
            where TFunctionalOptions : FunctionalOptions
        {
            var connection = await unitTestContext.Logical.Fabric.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);
            options.Use(connection);
            return options;
        }
    }
}
