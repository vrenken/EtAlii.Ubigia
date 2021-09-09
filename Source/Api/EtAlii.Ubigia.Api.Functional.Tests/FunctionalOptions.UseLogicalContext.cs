// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;

    public static class FunctionalTestContextUseLogicalContextExtension
    {
        public static FunctionalOptions UseLogicalContext(this FunctionalOptions options, FunctionalUnitTestContext context)
        {
            var logicalOptions = context.Logical
                .CreateLogicalOptionsWithoutConnection();

            return options.UseLogicalOptions(logicalOptions);
        }

        public static async Task<FunctionalOptions> UseLogicalContext(this FunctionalOptions options, FunctionalUnitTestContext context, bool openOnCreation)
        {
            var logicalOptions = await context.Logical
                .CreateLogicalOptionsWithConnection(openOnCreation)
                .ConfigureAwait(false);

            return options.UseLogicalOptions(logicalOptions);
        }
    }
}
