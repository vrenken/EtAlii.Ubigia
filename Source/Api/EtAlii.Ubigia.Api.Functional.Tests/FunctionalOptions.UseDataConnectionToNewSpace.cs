// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;

    public static class FunctionalTestContextUseTraversalDataConnectionToNewSpaceExtension
    {
        public static async Task<TFunctionalOptions> UseDataConnectionToNewSpace<TFunctionalOptions>(this TFunctionalOptions options, TraversalUnitTestContext unitTestContext, bool openOnCreation)
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

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System.Threading.Tasks;

    public static class FunctionalTestContextUseQueryingDataConnectionToNewSpaceExtension
    {
        public static async Task<TFunctionalOptions> UseDataConnectionToNewSpace<TFunctionalOptions>(this TFunctionalOptions options, QueryingUnitTestContext unitTestContext, bool openOnCreation)
            where TFunctionalOptions : FunctionalOptions
        {
            var connection = await unitTestContext.Functional.Logical.Fabric.Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .ConfigureAwait(false);
            options.Use(connection);
            return options;
        }
    }
}
